namespace MuseDashMirror.SourceGenerators.Analyzers.EventAnalyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class EventAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(
        EventAttributeInvalidReturnTypeError,
        EventAttributeNonStaticMethodForStaticConstructorError,
        EventAttributeInNonPartialClassError,
        RegisterInMuseDashMirrorSuggestion);

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

        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclaration);
        if (classSymbol is null)
        {
            return;
        }

        var classAttribute = classSymbol.GetAttributes()
            .FirstOrDefault(x => x.AttributeClass!.ToDisplayString() == RegisterInMuseDashMirrorAttributeName);

        var staticConstructor = false;
        if (classAttribute is not null)
        {
            staticConstructor = (RegisterMethodType)classAttribute.ConstructorArguments.Single().Value! == RegisterMethodType.StaticConstructor;
        }

        var methodSymbol = context.SemanticModel.GetDeclaredSymbol(methodDeclaration)!;

        if ((classSymbol.IsStatic || staticConstructor) && !methodSymbol.IsStatic)
        {
            context.ReportDiagnostic(Diagnostic.Create(EventAttributeNonStaticMethodForStaticConstructorError,
                methodDeclaration.Identifier.GetLocation(), methodSymbol.Name));
            return;
        }

        var methodAttribute = methodSymbol.GetAttributes().FirstOrDefault(x =>
            SceneEventRegex.IsMatch(x.AttributeClass!.ToDisplayString()) || PatchEventRegex.IsMatch(x.AttributeClass!.ToDisplayString()));

        if (methodAttribute is null)
        {
            return;
        }

        if (!modifiers.Any(SyntaxKind.PartialKeyword))
        {
            context.ReportDiagnostic(Diagnostic.Create(EventAttributeInNonPartialClassError, classDeclaration.Identifier.GetLocation(),
                classSymbol.Name, methodSymbol.Name));
            return;
        }

        var match = SceneEventRegex.IsMatch(methodAttribute.AttributeClass!.ToDisplayString())
            ? SceneEventRegex.Match(methodAttribute.AttributeClass!.ToDisplayString())
            : PatchEventRegex.Match(methodAttribute.AttributeClass!.ToDisplayString());

        var eventName = match.Groups[1].Value;

        if (returnType is not PredefinedTypeSyntax { Keyword.RawKind: (int)SyntaxKind.VoidKeyword })
        {
            context.ReportDiagnostic(Diagnostic.Create(EventAttributeInvalidReturnTypeError, methodDeclaration.Identifier.GetLocation(),
                methodSymbol.Name, eventName));
        }

        if (classAttribute is null)
        {
            context.ReportDiagnostic(Diagnostic.Create(RegisterInMuseDashMirrorSuggestion, classDeclaration.Identifier.GetLocation(),
                classSymbol.Name));
        }
    }
}