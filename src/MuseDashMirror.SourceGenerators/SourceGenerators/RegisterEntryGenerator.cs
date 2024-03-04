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

    private static bool FilterNode(SyntaxNode node, CancellationToken _) => node is ClassDeclarationSyntax;

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
            && types.Any(x => x.Type.ToString().StartsWith("MelonMod")))
        {
            MelonClassName = classDeclaration.Identifier.ValueText;
            MelonClassNameSpace = classSymbol.ContainingNamespace.ToDisplayString();
        }

        var @namespace = classSymbol.ContainingNamespace.ToDisplayString();
        var className = classSymbol.Name;

        var methodSymbols = classSymbol.GetMembers().OfType<IMethodSymbol>().ToList();
        var fieldSymbols = classSymbol.GetMembers().OfType<IFieldSymbol>().ToList();
        var propertySymbols = classSymbol.GetMembers().OfType<IPropertySymbol>().ToList();

        var registerMethodNames = ExtractMethodNames(methodSymbols, className)
            .Concat(ExtractFieldNames(fieldSymbols, className))
            .Concat(ExtractPropertyNames(propertySymbols, className))
            .ToList();

        return new RegisterClassData(@namespace, className, registerMethodNames);
    }

    private static void GenerateFromData(SourceProductionContext spc, ImmutableArray<RegisterClassData?> dataList)
    {
        if (MelonClassName is null || MelonClassNameSpace is null)
        {
            return;
        }

        using var usingStringBuilder = ZString.CreateStringBuilder();
        using var methodStringBuilder = ZString.CreateStringBuilder();
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
              {{usingStringBuilder.ToString()}}
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

    private static IEnumerable<string> ExtractMethodNames(IEnumerable<IMethodSymbol> methodSymbols, string className)
    {
        return methodSymbols.SelectMany(methodSymbol =>
        {
            var methodName = methodSymbol.Name;
            var attributes = methodSymbol.GetAttributes();
            var sceneEventNames = attributes
                .Select(static attribute => SceneEventRegex.Match(attribute.AttributeClass!.ToDisplayString()))
                .Where(static match => match.Success)
                .Select(static match => match.Groups[1].Value);
            var patchEventNames = attributes
                .Select(static attribute => PatchEventRegex.Match(attribute.AttributeClass!.ToDisplayString()))
                .Where(static match => match.Success)
                .Select(static match => match.Groups[1].Value);

            return sceneEventNames.Select(sceneEventName => $"Register{className}{methodName}To{sceneEventName}Event()")
                .Concat(patchEventNames.Select(patchEventName => $"Register{className}{methodName}To{patchEventName}Event()"));
        });
    }

    private static IEnumerable<string> ExtractFieldNames(IEnumerable<IFieldSymbol> fieldSymbols, string className)
    {
        return fieldSymbols
            .Where(fieldSymbol => fieldSymbol.GetAttributes().Any(x => x.AttributeClass!.ToDisplayString() == PnlMenuToggleAttributeName))
            .Select(fieldSymbol => $"Register{className}{fieldSymbol.Name}ToPnlMenuEvent()");
    }

    private static IEnumerable<string> ExtractPropertyNames(IEnumerable<IPropertySymbol> propertySymbols, string className)
    {
        return propertySymbols
            .Where(propertySymbol => propertySymbol.GetAttributes().Any(x => x.AttributeClass!.ToDisplayString() == PnlMenuToggleAttributeName))
            .Select(propertySymbol => $"Register{className}{propertySymbol.Name}ToPnlMenuEvent()");
    }

    private sealed record RegisterClassData(string Namespace, string ClassName, List<string> RegisterMethodNames);
}