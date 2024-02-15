namespace MuseDashMirror.SourceGenerators.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class LoggerAnalyzer : DiagnosticAnalyzer
{
    private const string DiagnosticId = "MDM0000";
    private const string Category = "Usage";
    private static readonly LocalizableString Title = GetLocalizableString(nameof(MDM0000Title));
    private static readonly LocalizableString MessageFormat = GetLocalizableString(nameof(MDM0000MessageFormat));
    private static readonly LocalizableString Description = GetLocalizableString(nameof(MDM0000Description));

    private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, true,
        Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ClassDeclaration);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not ClassDeclarationSyntax { Modifiers: var modifiers and not [] } node)
        {
            return;
        }

        var symbol = context.SemanticModel.GetDeclaredSymbol(node);
        if (symbol is null)
        {
            return;
        }

        var hasLoggerAttribute = symbol.GetAttributes().Any(x => x.AttributeClass!.ToDisplayString() == LoggerAttributeName);

        if (hasLoggerAttribute && !modifiers.Any(SyntaxKind.PartialKeyword))
        {
            context.ReportDiagnostic(Diagnostic.Create(Rule, node.Identifier.GetLocation(), symbol.Name));
        }
    }
}