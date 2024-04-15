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

        var sceneEventNames = symbol.GetAttributes()
            .Select(static attribute => SceneEventRegex.Match(attribute.AttributeClass!.ToDisplayString()))
            .Where(static match => match.Success)
            .Select(static match => match.Groups[1].Value)
            .ToArray();

        return new SceneEventData(symbol.ContainingNamespace.ToDisplayString(), parent.Identifier.ValueText, symbol.Name, sceneEventNames);
    }

    private static void GenerateFromData(SourceProductionContext spc, SceneEventData? data)
    {
        if (data is not var (@namespace, className, methodName, sceneEventNames))
        {
            return;
        }

        using var sceneEventStringBuilder = ZString.CreateStringBuilder(true);
        foreach (var sceneEventName in sceneEventNames)
        {
            sceneEventStringBuilder.AppendLine($"\t{GetGeneratedCodeAttribute(typeof(SceneEventGenerator))}");
            sceneEventStringBuilder.AppendLine(
                $"\tinternal static void Register{className}{methodName}To{sceneEventName}Event() => On{sceneEventName} += {methodName};");
            sceneEventStringBuilder.AppendLine();
        }

        spc.AddSource($"{className}.{methodName}.SceneEvents.g.cs",
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

    private sealed record SceneEventData(string Namespace, string ClassName, string MethodName, string[] SceneEventNames);
}