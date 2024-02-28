namespace MuseDashMirror.SourceGenerators.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PnlMenuToggleAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(
        PnlMenuToggleAttributeInNonePartialClassError,
        PnlMenuToggleAttributeOnNonGameObjectError,
        PnlMenuToggleAttributeOnNonStaticGameObjectError);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.FieldDeclaration);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        var symbol = context.SemanticModel.GetDeclaredSymbol(context.Node);
        if (symbol is null)
        {
            return;
        }

        if (context.Node.Parent is not ClassDeclarationSyntax { Modifiers: var modifiers and not [] } classDeclarationSyntax)
        {
            return;
        }

        var hasPnlMenuToggleAttribute = symbol.GetAttributes().Any(x => x.AttributeClass!.ToDisplayString() == PnlMenuToggleAttributeName);
        if (!hasPnlMenuToggleAttribute)
        {
            return;
        }

        if (!modifiers.Any(SyntaxKind.PartialKeyword))
        {
            context.ReportDiagnostic(Diagnostic.Create(PnlMenuToggleAttributeInNonePartialClassError, classDeclarationSyntax.Identifier.GetLocation(),
                classDeclarationSyntax.Identifier.ValueText, symbol.Name));
        }

        switch (symbol)
        {
            case IPropertySymbol propertySymbol when propertySymbol.Type.Name != "GameObject":
                context.ReportDiagnostic(Diagnostic.Create(PnlMenuToggleAttributeOnNonGameObjectError, symbol.Locations[0],
                    symbol.Name));
                break;

            case IFieldSymbol fieldSymbol when fieldSymbol.Type.Name != "GameObject":
                context.ReportDiagnostic(Diagnostic.Create(PnlMenuToggleAttributeOnNonGameObjectError, symbol.Locations[0],
                    symbol.Name));
                break;
        }

        if (!symbol.IsStatic)
        {
            context.ReportDiagnostic(Diagnostic.Create(PnlMenuToggleAttributeOnNonStaticGameObjectError, symbol.Locations[0],
                symbol.Name));
        }
    }
}