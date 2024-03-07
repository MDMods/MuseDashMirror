namespace MuseDashMirror.SourceGenerators.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PnlMenuToggleAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(
        PnlMenuToggleAttributeOnMultipleFieldsError,
        PnlMenuToggleAttributeInNonePartialClassError,
        PnlMenuToggleAttributeOnNonGameObjectError,
        PnlMenuToggleAttributeOnNonStaticGameObjectError);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.PropertyDeclaration, SyntaxKind.FieldDeclaration);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is FieldDeclarationSyntax { Declaration.Variables.Count: > 1 })
        {
            context.ReportDiagnostic(Diagnostic.Create(PnlMenuToggleAttributeOnMultipleFieldsError, context.Node.GetLocation(),
                context.ContainingSymbol!.Name));
        }

        if (context.Node.Parent is not ClassDeclarationSyntax { Modifiers: var modifiers and not [] } classDeclarationSyntax)
        {
            return;
        }

        var hasPnlMenuToggleAttribute = context.ContainingSymbol!.GetAttributes()
            .Any(x => x.AttributeClass!.ToDisplayString() == PnlMenuToggleAttributeName);
        if (!hasPnlMenuToggleAttribute)
        {
            return;
        }

        if (!modifiers.Any(SyntaxKind.PartialKeyword))
        {
            var location = GetClassDeclarationLocation(classDeclarationSyntax);
            context.ReportDiagnostic(Diagnostic.Create(PnlMenuToggleAttributeInNonePartialClassError, location,
                classDeclarationSyntax.Identifier.ValueText, context.ContainingSymbol.Name));
        }

        switch (context.ContainingSymbol)
        {
            case IPropertySymbol propertySymbol when propertySymbol.Type.Name != "GameObject":
                context.ReportDiagnostic(Diagnostic.Create(PnlMenuToggleAttributeOnNonGameObjectError, context.ContainingSymbol.Locations[0],
                    context.ContainingSymbol.Name));
                break;

            case IFieldSymbol fieldSymbol when fieldSymbol.Type.Name != "GameObject":
                context.ReportDiagnostic(Diagnostic.Create(PnlMenuToggleAttributeOnNonGameObjectError, context.ContainingSymbol.Locations[0],
                    context.ContainingSymbol.Name));
                break;
        }

        if (!context.ContainingSymbol.IsStatic)
        {
            context.ReportDiagnostic(Diagnostic.Create(PnlMenuToggleAttributeOnNonStaticGameObjectError, context.ContainingSymbol.Locations[0],
                context.ContainingSymbol.Name));
        }
    }
}