namespace MuseDashMirror.CodeAnalysis.Generators;

[Generator(LanguageNames.CSharp)]
public sealed class PatchEventGenerator : IIncrementalGenerator
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
        ParameterList.Parameters.Count: 2,
        Parent: ClassDeclarationSyntax
    };

    private static PatchEventData? ExtractDataFromContext(GeneratorSyntaxContext ctx, CancellationToken ct)
    {
        if (ctx.Node is not MethodDeclarationSyntax
            {
                ParameterList.Parameters: var parameters,
                Parent : ClassDeclarationSyntax parent
            })
        {
            return null;
        }

        var symbol = ctx.SemanticModel.GetDeclaredSymbol(ctx.Node, ct)!;

        var patchEventName = symbol.GetAttributes()
            .Select(static attribute => GetPatchEventName(attribute.AttributeClass))
            .FirstOrDefault(static eventName => eventName is not null);

        if (patchEventName is null)
        {
            return null;
        }

        if (parameters[1].Type is not IdentifierNameSyntax { Identifier.ValueText: var parameterType } ||
            parameterType != $"{patchEventName[..^5]}EventArgs")
        {
            return null;
        }

        return new PatchEventData(symbol.ContainingNamespace.ToDisplayString(), parent.Identifier.ValueText, symbol.Name, patchEventName);
    }

    private static void GenerateFromData(SourceProductionContext spc, PatchEventData? data)
    {
        if (data is not var (@namespace, className, methodName, patchEventName))
        {
            return;
        }

        spc.AddSource($"{className}.{methodName}.{patchEventName}Event.g.cs",
            Header +
            $$"""
              using static global::MuseDashMirror.PatchEvents;

              namespace {{@namespace}};

              partial class {{className}}
              {
                  {{GetGeneratedCodeAttribute(typeof(PatchEventGenerator))}}
                  internal static void Register{{className}}{{methodName}}To{{patchEventName}}Event() => {{patchEventName}} += {{methodName}};
              }

              """);
    }

    private sealed record PatchEventData(string Namespace, string ClassName, string MethodName, string PatchEventName);
}
