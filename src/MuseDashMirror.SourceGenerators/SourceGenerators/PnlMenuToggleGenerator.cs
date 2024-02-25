namespace MuseDashMirror.SourceGenerators;

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
        PropertyDeclarationSyntax
        {
            Type: IdentifierNameSyntax { Identifier.ValueText: "GameObject" },
            Parent: ClassDeclarationSyntax { Modifiers: var modifiers and not [] }
        } when modifiers.Any(SyntaxKind.PartialKeyword) => true,

        FieldDeclarationSyntax
        {
            Declaration.Type: IdentifierNameSyntax { Identifier.ValueText: "GameObject" },
            Parent: ClassDeclarationSyntax { Modifiers: var modifiers and not [] }
        } when modifiers.Any(SyntaxKind.PartialKeyword) => true,

        _ => false
    };

    private static PnlMenuToggleData? ExtractDataFromContext(GeneratorAttributeSyntaxContext ctx, CancellationToken _)
    {
        var symbol = ctx.TargetSymbol;
        if (symbol is not (IPropertySymbol or IFieldSymbol))
        {
            return null;
        }

        if (ctx.TargetNode.Parent is not ClassDeclarationSyntax { Identifier.ValueText: var className })
        {
            return null;
        }

        var @namespace = symbol.ContainingNamespace.ToDisplayString();
        var variableName = symbol.Name;
        var attribute = symbol.GetAttributes().First(x => x.AttributeClass!.ToDisplayString() == PnlMenuToggleAttributeName);

        if (attribute.ApplicationSyntaxReference!.GetSyntax() is not AttributeSyntax { ArgumentList.Arguments: var arguments })
        {
            return null;
        }

        var boolName = arguments[2].Expression.ToString().Contains("nameof(")
            ? arguments[2].Expression.ToString()[7..^1]
            : arguments[2].Expression.ToString();

        var constructorArguments = attribute.ConstructorArguments.Select(x => x.Value!.ToString()).Take(2).ToArray();

        return new PnlMenuToggleData(@namespace, className, variableName, constructorArguments[0], constructorArguments[1], boolName);
    }

    private static void GenerateFromData(SourceProductionContext spc, PnlMenuToggleData? data)
    {
        if (data is not var (@namespace, className, variableName, toggleName, toggleText, boolName))
        {
            return;
        }

        spc.AddSource($"{className}.{variableName}.PnlMenuToggle.g.cs",
            Header +
            $$"""
              using MuseDashMirror.EventArguments;
              using static global::MuseDashMirror.UIComponents.ToggleUtils;

              namespace {{@namespace}};

              partial class {{className}}
              {
                  {{GetGeneratedCodeAttribute(nameof(PnlMenuToggleGenerator))}}
                  private static void Register{{variableName}}PnlMenuToggle(object sender, PnlMenuEventArgs args)
                  {
                      {{variableName}} = CreatePnlMenuToggle("{{toggleName}}", "{{toggleText}}", val => {{boolName}} = val);
                  }
              }
              """);
    }

    private sealed record PnlMenuToggleData(
        string Namespace,
        string ClassName,
        string VariableName,
        string ToggleName,
        string ToggleText,
        string BoolName);
}