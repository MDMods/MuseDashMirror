namespace MuseDashMirror.SourceGenerators;

[Generator(LanguageNames.CSharp)]
public sealed class RegisterEntryGenerator : IIncrementalGenerator
{
    private static string? MelonClassName { get; set; }
    private static string? MelonClassNameSpace { get; set; }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(
            context.SyntaxProvider.CreateSyntaxProvider(
                FilterNode, ExtractDataFromContext).Collect(),
            GenerateFromData);
    }

    private static bool FilterNode(SyntaxNode node, CancellationToken _) =>
        node is ClassDeclarationSyntax { Modifiers: var modifiers } && modifiers.Any(SyntaxKind.PartialKeyword);

    private static RegisterClassData? ExtractDataFromContext(GeneratorSyntaxContext context, CancellationToken _)
    {
        if (context is not
            {
                Node: ClassDeclarationSyntax classDeclaration,
                SemanticModel: var semanticModel
            })
        {
            return null;
        }

        var classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration)!;

        if (classDeclaration is { BaseList.Types: var types }
            && types.Any(x => x.Type.ToString().Contains("MelonMod")))
        {
            MelonClassName = classDeclaration.Identifier.ValueText;
            MelonClassNameSpace = classSymbol.ContainingNamespace.ToDisplayString();
            return null;
        }

        var @namespace = classSymbol.ContainingNamespace.ToDisplayString();
        var className = classSymbol.Name;
        var methodSymbols = classSymbol.GetMembers().OfType<IMethodSymbol>();

        List<string> registerMethodNames = [];
        foreach (var methodSymbol in methodSymbols)
        {
            var methodName = methodSymbol.Name;
            var attributes = methodSymbol.GetAttributes();
            var sceneEventNames = attributes
                .Select(static attribute => SceneEventRegex.Match(attribute.AttributeClass!.ToDisplayString()))
                .Where(static match => match.Success)
                .Select(static match => match.Groups[1].Value)
                .ToList();
            var patchEventNames = attributes
                .Select(static attribute => PatchEventRegex.Match(attribute.AttributeClass!.ToDisplayString()))
                .Where(static match => match.Success)
                .Select(static match => match.Groups[1].Value)
                .ToList();

            registerMethodNames.AddRange(sceneEventNames.Select(sceneEventName => $"Register{className}{methodName}To{sceneEventName}Event()"));
            registerMethodNames.AddRange(patchEventNames.Select(patchEventName => $"Register{className}{methodName}To{patchEventName}Event()"));
        }

        return new RegisterClassData(@namespace, className, registerMethodNames);
    }

    private static void GenerateFromData(SourceProductionContext spc, ImmutableArray<RegisterClassData?> dataList)
    {
        if (MelonClassName is null || MelonClassNameSpace is null)
        {
            return;
        }

        var usingStringBuilder = new StringBuilder();
        var methodStringBuilder = new StringBuilder();
        foreach (var data in dataList)
        {
            if (data is not var (@namespace, className, registerMethodNames) || registerMethodNames is [])
            {
                continue;
            }

            usingStringBuilder.AppendLine($"using static global::{@namespace}.{className};");
            foreach (var registerMethodName in registerMethodNames)
            {
                methodStringBuilder.AppendLine($"\t\t{registerMethodName};");
            }
        }

        spc.AddSource($"{MelonClassName}.RegisterEntry.g.cs",
            Header +
            $$"""
              {{usingStringBuilder}}
              namespace {{MelonClassNameSpace}};

              partial class {{MelonClassName}}
              {
                  {{GetGeneratedCodeAttribute(nameof(RegisterEntryGenerator))}}
                  static {{MelonClassName}}()
                  {
              {{methodStringBuilder.ToString().TrimEnd()}}
                  }
              }
              """);
    }

    private sealed record RegisterClassData(string Namespace, string ClassName, List<string> RegisterMethodNames);
}