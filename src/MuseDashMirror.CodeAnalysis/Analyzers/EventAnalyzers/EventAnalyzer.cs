namespace MuseDashMirror.CodeAnalysis.Analyzers.EventAnalyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class EventAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
    [
        EventAttributeInvalidReturnTypeError,
        EventAttributeNonStaticMethodForStaticConstructorError
    ];

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

        if (context.ContainingSymbol is null)
        {
            return;
        }

        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclaration, context.CancellationToken);
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

        var eventName = context.ContainingSymbol.GetAttributes()
            .Select(static attribute => GetSceneEventName(attribute.AttributeClass) ?? GetPatchEventName(attribute.AttributeClass))
            .FirstOrDefault(static name => name is not null);

        if (eventName is null)
        {
            return;
        }

        if (returnType is not PredefinedTypeSyntax { Keyword.RawKind: (int)SyntaxKind.VoidKeyword })
        {
            context.ReportDiagnostic(Diagnostic.Create(EventAttributeInvalidReturnTypeError, methodDeclaration.Identifier.GetLocation(),
                context.ContainingSymbol.Name, eventName));
        }
    }
}
