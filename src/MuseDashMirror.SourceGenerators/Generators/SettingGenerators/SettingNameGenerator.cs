namespace MuseDashMirror.SourceGenerators.SettingGenerators;

[Generator(LanguageNames.CSharp)]
public sealed class SettingNameGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(context.SyntaxProvider.ForAttributeWithMetadataName(
                SettingNameAttributeName, FilterNode, ExtractDataFromContext),
            GenerateFromData);
    }

    private static bool FilterNode(SyntaxNode node, CancellationToken _) =>
        node is ClassDeclarationSyntax { Modifiers: var modifiers and not [] } && modifiers.Any(SyntaxKind.PartialKeyword);

    private static SettingNameData? ExtractDataFromContext(GeneratorAttributeSyntaxContext ctx, CancellationToken _)
    {
        if (ctx is not
            {
                TargetNode: ClassDeclarationSyntax,
                TargetSymbol: INamedTypeSymbol { Name: var className, ContainingNamespace: var @namespace },
                Attributes: var attributes
            })
        {
            return null;
        }

        var attribute = attributes.First(static x => x.AttributeClass!.ToDisplayString() == SettingNameAttributeName);
        var name = (string)attribute.ConstructorArguments[0].Value!;
        var description = (string?)attribute.ConstructorArguments[1].Value;
        return new SettingNameData(name, description);
    }

    private static void GenerateFromData(SourceProductionContext spc, SettingNameData? data)
    {
        if (data is not var (name, description))
        {
        }
    }

    private record SettingNameData(string Name, string? Description);
}