using System.Collections.Immutable;

namespace MuseDashMirror.SourceGenerators.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class LoggerAnalyzer : DiagnosticAnalyzer
{
    private const string DiagnosticId = "MM0000";
    private const string Title = "LoggerAttribute should be used on partial class";
    private const string MessageFormat = "Class {0} must have partial keyword in its declaration";
    private const string Category = "SourceGenerator";
    private const string Description = "LoggerAttribute should be used on partial class.";

    private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, true,
        Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(Rule);

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