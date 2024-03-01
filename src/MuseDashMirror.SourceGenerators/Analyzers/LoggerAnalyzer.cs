namespace MuseDashMirror.SourceGenerators.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class LoggerAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(LoggerAttributeForNonPartialClassError);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ClassDeclaration);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not ClassDeclarationSyntax { Modifiers: var modifiers and not [] } classDeclaration)
        {
            return;
        }

        var symbol = context.SemanticModel.GetDeclaredSymbol(classDeclaration);
        if (symbol is null)
        {
            return;
        }

        var hasAttribute = symbol.GetAttributes().Any(x => x.AttributeClass!.ToDisplayString() == LoggerAttributeName);

        if (!hasAttribute || modifiers.Any(SyntaxKind.PartialKeyword))
        {
            return;
        }

        var location = GetClassDeclarationLocation(classDeclaration);
        context.ReportDiagnostic(Diagnostic.Create(LoggerAttributeForNonPartialClassError, location, symbol.Name));
    }
}