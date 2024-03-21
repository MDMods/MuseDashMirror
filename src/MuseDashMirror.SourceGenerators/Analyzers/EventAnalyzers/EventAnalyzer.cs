namespace MuseDashMirror.SourceGenerators.Analyzers.EventAnalyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class EventAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(
        EventAttributeInvalidReturnTypeError,
        EventAttributeNonStaticMethodForStaticConstructorError,
        EventAttributeInNonPartialClassError);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.MethodDeclaration);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not MethodDeclarationSyntax
            {
                ReturnType: var returnType,
                Parent: ClassDeclarationSyntax { Modifiers: var modifiers and not [] } classDeclaration
            } methodDeclaration)
        {
            return;
        }

        if (context.ContainingSymbol is null)
        {
            return;
        }

        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclaration);
        if (classSymbol is null)
        {
            return;
        }

        if (classSymbol.IsStatic && !context.ContainingSymbol.IsStatic)
        {
            context.ReportDiagnostic(Diagnostic.Create(EventAttributeNonStaticMethodForStaticConstructorError,
                methodDeclaration.Identifier.GetLocation(), context.ContainingSymbol.Name));
            return;
        }

        var methodAttribute = context.ContainingSymbol.GetAttributes().FirstOrDefault(x =>
            SceneEventRegex.IsMatch(x.AttributeClass!.ToDisplayString()) || PatchEventRegex.IsMatch(x.AttributeClass!.ToDisplayString()));

        if (methodAttribute is null)
        {
            return;
        }

        if (!modifiers.Any(SyntaxKind.PartialKeyword))
        {
            var location = GetClassDeclarationLocation(classDeclaration);
            context.ReportDiagnostic(Diagnostic.Create(EventAttributeInNonPartialClassError, location,
                classSymbol.Name, context.ContainingSymbol.Name));
            return;
        }

        var match = SceneEventRegex.IsMatch(methodAttribute.AttributeClass!.ToDisplayString())
            ? SceneEventRegex.Match(methodAttribute.AttributeClass!.ToDisplayString())
            : PatchEventRegex.Match(methodAttribute.AttributeClass!.ToDisplayString());

        var eventName = match.Groups[1].Value;

        if (returnType is not PredefinedTypeSyntax { Keyword.RawKind: (int)SyntaxKind.VoidKeyword })
        {
            context.ReportDiagnostic(Diagnostic.Create(EventAttributeInvalidReturnTypeError, methodDeclaration.Identifier.GetLocation(),
                context.ContainingSymbol.Name, eventName));
        }
    }
}