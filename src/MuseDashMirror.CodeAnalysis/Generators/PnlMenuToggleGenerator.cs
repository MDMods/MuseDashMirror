namespace MuseDashMirror.CodeAnalysis.Generators;

[Generator]
public sealed class PnlMenuToggleGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(
            context.SyntaxProvider.ForAttributeWithMetadataName(
                PnlMenuToggleAttributeName, FilterNode, ExtractDataFromContext),
            GenerateFromData);
    }

    private static bool FilterNode(SyntaxNode node, CancellationToken _) => node switch
    {
        PropertyDeclarationSyntax { Parent: ClassDeclarationSyntax } => true,
        VariableDeclaratorSyntax { Parent: VariableDeclarationSyntax { Parent: FieldDeclarationSyntax { Parent: ClassDeclarationSyntax } } } => true,

        _ => false
    };

    private static PnlMenuToggleData? ExtractDataFromContext(GeneratorAttributeSyntaxContext ctx, CancellationToken ct)
    {
        var targetType = GetFieldOrPropertyType(ctx.TargetSymbol);
        var gameObjectType = ctx.SemanticModel.Compilation.GetTypeByMetadataName("UnityEngine.GameObject");

        if (targetType is null || gameObjectType is null || !SymbolEqualityComparer.Default.Equals(targetType, gameObjectType))
        {
            return null;
        }

        if (ctx.TargetSymbol is not { IsStatic: true } targetSymbol)
        {
            return null;
        }

        var attribute = ctx.Attributes[0];

        if (attribute.ConstructorArguments is not { Length: 3 or 4 } arguments ||
            arguments[0].Value is not string toggleName ||
            arguments[1].Value is not string toggleText ||
            attribute.ApplicationSyntaxReference?.GetSyntax(ct) is not AttributeSyntax { ArgumentList.Arguments: var attributeArguments } ||
            attributeArguments.Count != arguments.Length ||
            GetNameofMemberExpression(ctx.SemanticModel, attributeArguments[2].Expression, ct) is not { } boolMemberExpression)
        {
            return null;
        }

        var toggleGroupExpression = attributeArguments.Count == 4
            ? GetNameofMemberExpression(ctx.SemanticModel, attributeArguments[3].Expression, ct)
            : "null";

        if (toggleGroupExpression is null)
        {
            return null;
        }

        return new PnlMenuToggleData(
            targetSymbol.ContainingNamespace.ToDisplayString(),
            targetSymbol.ContainingType.Name,
            targetSymbol.Name,
            toggleName,
            toggleText,
            boolMemberExpression,
            toggleGroupExpression);
    }

    private static void GenerateFromData(SourceProductionContext spc, PnlMenuToggleData? data)
    {
        if (data is not var (@namespace, className, variableName, toggleName, toggleText, boolMemberExpression, toggleGroupExpression))
        {
            return;
        }

        spc.AddSource($"{className}.{variableName}.PnlMenuToggle.g.cs",
            Header +
            $$"""
              namespace {{@namespace}};

              partial class {{className}}
              {
                  {{GetGeneratedCodeAttribute(typeof(PnlMenuToggleGenerator))}}
                  internal static void Register{{className}}{{variableName}}ToPnlMenuEvent() =>
                      global::MuseDashMirror.PatchEvents.PnlMenuPatch += (_, _) =>
                          {{variableName}} = global::MuseDashMirror.UI.ToggleUtils.CreatePnlMenuToggle(
                              new global::MuseDashMirror.Models.ToggleParameters(
                                  "{{toggleName}}",
                                  new global::MuseDashMirror.Models.TextParameters("{{toggleText}}"),
                                  {{boolMemberExpression}},
                                  value => {{boolMemberExpression}} = value,
                                  {{toggleGroupExpression}}));
              }
              """);
    }

    private sealed record PnlMenuToggleData(
        string Namespace,
        string ClassName,
        string VariableName,
        string ToggleName,
        string ToggleText,
        string BoolMemberExpression,
        string ToggleGroupExpression);
}
