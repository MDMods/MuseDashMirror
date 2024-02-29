namespace MuseDashMirror.SourceGenerators.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class MelonModClassAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(InheritedMelonModClassNonPartialError);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ClassDeclaration);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not ClassDeclarationSyntax
            {
                Modifiers: var modifiers and not [],
                BaseList.Types: var types and not []
            } classDeclaration ||
            !types.Any(x => x.Type.ToString().StartsWith("MelonMod")))
        {
            return;
        }

        if (context.ContainingSymbol!.Locations.Length > 1 && !modifiers.Any(SyntaxKind.PartialKeyword))
        {
            context.ReportDiagnostic(Diagnostic.Create(InheritedMelonModClassNonPartialError, classDeclaration.Identifier.GetLocation(),
                classDeclaration.Identifier.ValueText));
        }
    }
}