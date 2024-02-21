namespace MuseDashMirror.SourceGenerators.Analyzers.EventAnalyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class SceneEventAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(SceneEventAttributeInvalidArgsError);

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

        var methodSymbol = context.SemanticModel.GetDeclaredSymbol(methodDeclaration)!;
        var attribute = methodSymbol.GetAttributes().FirstOrDefault(x => SceneEventRegex.IsMatch(x.AttributeClass!.ToDisplayString()));
        if (attribute is null)
        {
            return;
        }

        var match = SceneEventRegex.Match(attribute.AttributeClass!.ToDisplayString());
        var sceneEventName = match.Groups[1].Value;

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
                methodSymbol.Name, sceneEventName));
        }
    }
}