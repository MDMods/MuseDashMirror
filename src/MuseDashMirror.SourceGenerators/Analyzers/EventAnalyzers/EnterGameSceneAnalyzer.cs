namespace MuseDashMirror.SourceGenerators.Analyzers.EventAnalyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class EnterGameSceneAnalyzer : DiagnosticAnalyzer
{
    private const string DiagnosticId = "MDM0103";
    private const string Category = "Usage";
    private static readonly LocalizableString Title = GetLocalizableString(nameof(MDM0103Title));
    private static readonly LocalizableString MessageFormat = GetLocalizableString(nameof(MDM0103MessageFormat));
    private static readonly LocalizableString Description = GetLocalizableString(nameof(MDM0103Description));

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

        var hasAttribute = symbol.GetAttributes().Any(x => x.AttributeClass!.ToDisplayString() == EnterGameSceneAttributeName);
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