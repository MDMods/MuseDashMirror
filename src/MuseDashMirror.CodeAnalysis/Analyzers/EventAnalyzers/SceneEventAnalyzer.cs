namespace MuseDashMirror.CodeAnalysis.Analyzers.EventAnalyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class SceneEventAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [SceneEventAttributeInvalidArgsError];

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
                ParameterList.Parameters: var parameters,
                Parent: ClassDeclarationSyntax
            } methodDeclaration)
        {
            return;
        }

        var sceneEventName = context.ContainingSymbol!.GetAttributes()
            .Select(static attribute => GetSceneEventName(attribute.AttributeClass))
            .FirstOrDefault(static eventName => eventName is not null);

        if (sceneEventName is null)
        {
            return;
        }

        var correctParameters = parameters is
        [
            {
                Type: PredefinedTypeSyntax
                {
                    Keyword.RawKind: (int)SyntaxKind.ObjectKeyword
                }
            },
            {
                Type: IdentifierNameSyntax
                {
                    Identifier.ValueText: "SceneEventArgs"
                }
            }
        ];

        if (!correctParameters)
        {
            context.ReportDiagnostic(Diagnostic.Create(SceneEventAttributeInvalidArgsError, methodDeclaration.Identifier.GetLocation(),
                context.ContainingSymbol.Name, sceneEventName));
        }
    }
}
