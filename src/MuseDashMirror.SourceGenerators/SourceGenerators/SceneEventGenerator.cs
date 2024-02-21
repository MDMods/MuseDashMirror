namespace MuseDashMirror.SourceGenerators;

[Generator(LanguageNames.CSharp)]
public sealed class SceneEventGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(
            context.SyntaxProvider.CreateSyntaxProvider(
                FilterNode, ExtractDataFromContext),
            GenerateFromData);
    }

    private static bool FilterNode(SyntaxNode node, CancellationToken _) => node is MethodDeclarationSyntax
    {
        ReturnType: PredefinedTypeSyntax
        {
            Keyword.RawKind: (int)SyntaxKind.VoidKeyword
        },
        ParameterList.Parameters:
        [
            {
                Type: PredefinedTypeSyntax
                {
                    Keyword.RawKind: (int)SyntaxKind.ObjectKeyword
                }
            },
            {
                Type: IdentifierNameSyntax
                {
                    Identifier.ValueText: "SceneEventArgs"
                }
            }
        ],
        Parent: ClassDeclarationSyntax
        {
            Modifiers: var classModifiers and not []
        }
    } && classModifiers.Any(SyntaxKind.PartialKeyword);

    private static SceneEventData? ExtractDataFromContext(GeneratorSyntaxContext ctx, CancellationToken _)
    {
        if (ctx.Node is not MethodDeclarationSyntax { Parent : ClassDeclarationSyntax parent })
        {
            return null;
        }

        var symbol = ctx.SemanticModel.GetDeclaredSymbol(ctx.Node)!;

        var sceneEvents = symbol.GetAttributes()
            .Select(static attribute => SceneEventRegex.Match(attribute.AttributeClass!.ToDisplayString()))
            .Select(static match => match.Groups[1].Value)
            .ToArray();

        return new SceneEventData(symbol.ContainingNamespace.ToString(), parent.Identifier.ValueText, symbol.Name, sceneEvents);
    }

    private static void GenerateFromData(SourceProductionContext spc, SceneEventData? data)
    {
        if (data is not var (@namespace, className, methodName, sceneEvents))
        {
            return;
        }

        var sceneEventStringBuilder = new StringBuilder();
        foreach (var sceneEvent in sceneEvents)
        {
            sceneEventStringBuilder.AppendLine($"\t{GetGeneratedCodeAttribute(nameof(SceneEventGenerator))}");
            sceneEventStringBuilder.AppendLine($"\tprivate static void Register{methodName}To{sceneEvent}Event() => On{sceneEvent} += {methodName};");
            sceneEventStringBuilder.AppendLine();
        }

        spc.AddSource($"{methodName}_SceneEvents.g.cs",
            Header +
            $$"""
              using static global::MuseDashMirror.SceneInfo;

              namespace {{@namespace}};

              partial class {{className}}
              {
              {{sceneEventStringBuilder.ToString().TrimEnd()}}
              }
              """);
    }

    private sealed record SceneEventData(string Namespace, string ClassName, string MethodName, string[] SceneEvents);
}