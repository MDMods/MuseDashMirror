namespace MuseDashMirror.CodeAnalysis;

internal static class Utils
{
    internal static ITypeSymbol? GetFieldOrPropertyType(ISymbol symbol) => symbol switch
    {
        IFieldSymbol fieldSymbol => fieldSymbol.Type,
        IPropertySymbol propertySymbol => propertySymbol.Type,
        _ => null
    };

    internal static string? GetNameofMemberExpression(
        SemanticModel semanticModel,
        ExpressionSyntax expression,
        CancellationToken cancellationToken)
    {
        if (expression is not InvocationExpressionSyntax
            {
                Expression: IdentifierNameSyntax { Identifier.ValueText: "nameof" },
                ArgumentList.Arguments: [{ Expression: var memberExpression }]
            })
        {
            return null;
        }

        var symbol = semanticModel.GetSymbolInfo(memberExpression, cancellationToken).Symbol;
        return symbol is IFieldSymbol or IPropertySymbol
            ? symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat.WithMemberOptions(SymbolDisplayMemberOptions.IncludeContainingType))
            : null;
    }

    internal static LocalizableString GetLocalizableString(string name) =>
        new LocalizableResourceString(name, ResourceManager, typeof(Resources));
}
