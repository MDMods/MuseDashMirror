namespace MuseDashMirror.SourceGenerators;

[Generator(LanguageNames.CSharp)]
public sealed class LoggerGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(
            context.SyntaxProvider.ForAttributeWithMetadataName(
                LoggerAttributeName, FilterNode, ExtractDataFromContext),
            GenerateFromData);
    }

    private static bool FilterNode(SyntaxNode node, CancellationToken _) =>
        node is ClassDeclarationSyntax { Modifiers: var modifiers and not [] } && modifiers.Any(SyntaxKind.PartialKeyword);

    private static LoggerData? ExtractDataFromContext(GeneratorAttributeSyntaxContext ctx, CancellationToken _)
    {
        if (ctx is not
            {
                TargetNode: ClassDeclarationSyntax,
                TargetSymbol: INamedTypeSymbol { IsStatic: var isStatic, Name: var className, ContainingNamespace: var @namespace },
                Attributes: var attributes
            })
        {
            return null;
        }

        var attribute = attributes.First(x => x.AttributeClass!.ToDisplayString() == LoggerAttributeName);

        var loggerType = isStatic ? LoggerType.StaticReadonly : LoggerType.Readonly;
        if (attribute.ConstructorArguments is not [] and var argument)
        {
            loggerType = (LoggerType)argument.Single().Value!;
        }

        var modifier = loggerType switch
        {
            LoggerType.Readonly => "readonly",
            LoggerType.StaticReadonly => "static readonly",
            _ => string.Empty
        };

        return new LoggerData(@namespace.ToDisplayString(), className, modifier);
    }

    private static void GenerateFromData(SourceProductionContext spc, LoggerData? data)
    {
        if (data is not var (@namespace, className, modifier))
        {
            return;
        }

        spc.AddSource($"{className}_Logger.g.cs",
            Header +
            $$"""
              namespace {{@namespace}};

              partial class {{className}}
              {
                  {{GetGeneratedCodeAttribute(nameof(LoggerGenerator))}}
                  private {{modifier}} global::MelonLoader.MelonLogger.Instance Logger = new("{{className}}");
              }
              """);
    }

    private sealed record LoggerData(string Namespace, string ClassName, string Modifier);
}