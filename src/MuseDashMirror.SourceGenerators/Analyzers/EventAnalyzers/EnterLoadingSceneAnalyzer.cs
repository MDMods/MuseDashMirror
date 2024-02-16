namespace MuseDashMirror.SourceGenerators.Analyzers.EventAnalyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class EnterLoadingSceneAnalyzer : DiagnosticAnalyzer
{
    private const string DiagnosticId = "MDM0104";
    private const string Category = "Usage";
    private static readonly LocalizableString Title = GetLocalizableString(nameof(MDM0104Title));
    private static readonly LocalizableString MessageFormat = GetLocalizableString(nameof(MDM0104MessageFormat));
    private static readonly LocalizableString Description = GetLocalizableString(nameof(MDM0104Description));

    private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, true,
        Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.MethodDeclaration);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not MethodDeclarationSyntax { ParameterList.Parameters: var parameters } node)
        {
            return;
        }

        var symbol = context.SemanticModel.GetDeclaredSymbol(node);
        if (symbol is null)
        {
            return;
        }

        var hasAttribute = symbol.GetAttributes().Any(x => x.AttributeClass!.ToDisplayString() == EnterLoadingSceneAttributeName);
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

        if (hasAttribute && !correctParameters)
        {
            context.ReportDiagnostic(Diagnostic.Create(Rule, node.Identifier.GetLocation(), symbol.Name));
        }
    }
}