namespace MuseDashMirror.SourceGenerators.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class EventAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(
        EventAttributeInvalidReturnTypeError,
        EventAttributeNonStaticMethodForStaticConstructorError);

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
                Parent: ClassDeclarationSyntax classDeclaration
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

        var attribute = methodSymbol.GetAttributes().FirstOrDefault(x => SceneEventRegex.IsMatch(x.AttributeClass!.ToDisplayString()));
        if (attribute is null)
        {
            return;
        }

        var match = SceneEventRegex.Match(attribute.AttributeClass!.ToDisplayString());
        var sceneEventName = match.Groups[1].Value;

        if (returnType is not PredefinedTypeSyntax { Keyword.RawKind: (int)SyntaxKind.VoidKeyword })
        {
            context.ReportDiagnostic(Diagnostic.Create(EventAttributeInvalidReturnTypeError, methodDeclaration.Identifier.GetLocation(),
                methodSymbol.Name, sceneEventName));
        }
    }
}