namespace MuseDashMirror.CodeAnalysis.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PnlMenuToggleAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
    [
        PnlMenuToggleAttributeOnMultipleFieldsError,
        PnlMenuToggleAttributeOnNonGameObjectError,
        PnlMenuToggleAttributeOnNonStaticGameObjectError,
        PnlMenuToggleAttributeArgumentIsNotNameofError
    ];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.PropertyDeclaration, SyntaxKind.FieldDeclaration);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (GetTargetSymbol(context) is not { } targetSymbol)
        {
            return;
        }

        var attribute = targetSymbol.GetAttributes().FirstOrDefault(x => x.AttributeClass?.ToDisplayString() == PnlMenuToggleAttributeName);
        if (attribute is null)
        {
            return;
        }

        if (context.Node is FieldDeclarationSyntax { Declaration.Variables.Count: > 1 })
        {
            context.ReportDiagnostic(Diagnostic.Create(PnlMenuToggleAttributeOnMultipleFieldsError, context.Node.GetLocation(), targetSymbol.Name));
        }

        var targetType = GetFieldOrPropertyType(targetSymbol);
        var gameObjectType = context.Compilation.GetTypeByMetadataName("UnityEngine.GameObject");
        if (gameObjectType is not null && !SymbolEqualityComparer.Default.Equals(targetType, gameObjectType))
        {
            context.ReportDiagnostic(Diagnostic.Create(PnlMenuToggleAttributeOnNonGameObjectError, targetSymbol.Locations[0], targetSymbol.Name));
        }

        if (!targetSymbol.IsStatic)
        {
            context.ReportDiagnostic(Diagnostic.Create(PnlMenuToggleAttributeOnNonStaticGameObjectError, targetSymbol.Locations[0], targetSymbol.Name));
        }

        AnalyzeNameofArguments(context, attribute);
    }

    private static ISymbol? GetTargetSymbol(SyntaxNodeAnalysisContext context) => context.Node switch
    {
        PropertyDeclarationSyntax propertyDeclaration => context.SemanticModel.GetDeclaredSymbol(propertyDeclaration, context.CancellationToken),
        FieldDeclarationSyntax { Declaration.Variables: { Count: > 0 } variables } =>
            context.SemanticModel.GetDeclaredSymbol(variables[0], context.CancellationToken),
        _ => null
    };

    private static void AnalyzeNameofArguments(SyntaxNodeAnalysisContext context, AttributeData attribute)
    {
        if (attribute.ApplicationSyntaxReference?.GetSyntax(context.CancellationToken) is not AttributeSyntax
            {
                ArgumentList.Arguments: var arguments
            })
        {
            return;
        }

        for (var index = 2; index < arguments.Count; index++)
        {
            var expression = arguments[index].Expression;
            if (GetNameofMemberExpression(context.SemanticModel, expression, context.CancellationToken) is null)
            {
                context.ReportDiagnostic(Diagnostic.Create(PnlMenuToggleAttributeArgumentIsNotNameofError, expression.GetLocation(), index + 1));
            }
        }
    }
}
