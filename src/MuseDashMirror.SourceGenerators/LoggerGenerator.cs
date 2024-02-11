namespace MuseDashMirror.SourceGenerators;

[Generator(LanguageNames.CSharp)]
public sealed class LoggerGenerator : IIncrementalGenerator
{
    private const string LoggerAttributeName = "MuseDashMirror.Attributes.LoggerAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(
            context.SyntaxProvider.ForAttributeWithMetadataName(
                LoggerAttributeName, FilterNode, ExtractDataFromContext), GenerateFromData);
    }

    private static bool FilterNode(SyntaxNode node, CancellationToken _) =>
        node is ClassDeclarationSyntax { Modifiers: var modifiers and not [] } && modifiers.Any(SyntaxKind.PartialKeyword);

    private static LoggerData? ExtractDataFromContext(GeneratorAttributeSyntaxContext ctx, CancellationToken _)
    {
        if (ctx is not
            {
                TargetNode: ClassDeclarationSyntax node,
                TargetSymbol: INamedTypeSymbol { IsStatic: var isStatic, Name: var typeName, ContainingNamespace: var @namespace },
                Attributes: var attributes
            })
        {
            return null;
        }

        var attribute = attributes.First(x => x.AttributeClass!.ToDisplayString() == LoggerAttributeName);
        var value = isStatic ? 1 : 0;
        if (attribute.ConstructorArguments is not [] and var argument)
        {
            value = (int)argument.First().Value!;
        }

        var modifier = value switch
        {
            0 => "readonly",
            1 => "static readonly",
            _ => string.Empty
        };

        return new LoggerData(@namespace.ToDisplayString(), typeName, modifier, node);
    }

    private static void GenerateFromData(SourceProductionContext spc, LoggerData? data)
    {
        if (data is not var (@namespace, className, modifier, classDeclaration))
        {
            return;
        }

        spc.AddSource(classDeclaration.Identifier.ValueText + "_Logger.g.cs",
            SourceGenerationTexts.Header +
            $$"""
              namespace {{@namespace}};

              partial class {{className}}
              {
                  private {{modifier}} global::MelonLoader.MelonLogger.Instance Logger = new("{{className}}");
              }
              """);
    }

    private sealed record LoggerData(string Namespace, string ClassName, string Modifier, ClassDeclarationSyntax ClassDeclaration);
}