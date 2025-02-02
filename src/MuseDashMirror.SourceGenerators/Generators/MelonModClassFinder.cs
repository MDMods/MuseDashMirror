namespace MuseDashMirror.SourceGenerators;

[Generator(LanguageNames.CSharp)]
public class MelonModClassFinder : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(
            context.SyntaxProvider.ForAttributeWithMetadataName(
                MelonInfoAttributeName, FilterNode, ExtractDataFromContext), (_, _) => { });
    }

    private static bool FilterNode(SyntaxNode node, CancellationToken _) => node is CompilationUnitSyntax;

    private static object? ExtractDataFromContext(GeneratorAttributeSyntaxContext ctx, CancellationToken _)
    {
        if (ctx is not
            {
                Attributes:
                [
                    {
                        ConstructorArguments: var arguments
                    }
                ]
            })
        {
            return null;
        }

        if (arguments[0].Value is not INamedTypeSymbol symbol)
        {
            return null;
        }

        var className = symbol.Name;
        var @namespace = symbol.ContainingNamespace.ToDisplayString();

        RegisterEntryGenerator.MelonModClassName = className;
        RegisterEntryGenerator.MelonModNamespace = @namespace;

        return null;
    }
}