namespace MuseDashMirror.SourceGenerators;

[Generator(LanguageNames.CSharp)]
public sealed class RegisterEntryGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var melonModClasses = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                MelonInfoAttributeName,
                static (_, _) => true,
                ExtractMelonModClassData)
            .Where(static data => data is not null)
            .Select(static (data, _) => data!)
            .Collect();

        var registerClasses = context.SyntaxProvider
            .CreateSyntaxProvider(FilterNode, ExtractRegisterClassData)
            .Where(static data => data is not null)
            .Select(static (data, _) => data!)
            .Collect();

        context.RegisterSourceOutput(
            melonModClasses.Combine(registerClasses),
            GenerateFromData);
    }

    private static bool FilterNode(SyntaxNode node, CancellationToken _) => node is ClassDeclarationSyntax;

    private static MelonModClassData? ExtractMelonModClassData(GeneratorAttributeSyntaxContext context, CancellationToken _)
    {
        var attribute = context.Attributes.FirstOrDefault();
        if (attribute is null || attribute.ConstructorArguments.Length == 0 ||
            attribute.ConstructorArguments[0].Value is not INamedTypeSymbol melonModClass)
        {
            return null;
        }

        var @namespace = melonModClass.ContainingNamespace.IsGlobalNamespace
            ? string.Empty
            : melonModClass.ContainingNamespace.ToDisplayString();

        return new MelonModClassData(@namespace, melonModClass.Name);
    }

    private static RegisterClassData? ExtractRegisterClassData(GeneratorSyntaxContext context, CancellationToken ct)
    {
        if (context is not
            {
                Node: ClassDeclarationSyntax classDeclaration,
                SemanticModel: var semanticModel
            })
        {
            return null;
        }

        var classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration, ct)!;
        var className = classSymbol.Name;

        var methodSymbols = classSymbol.GetMembers().OfType<IMethodSymbol>().ToList();
        var fieldSymbols = classSymbol.GetMembers().OfType<IFieldSymbol>().ToList();
        var propertySymbols = classSymbol.GetMembers().OfType<IPropertySymbol>().ToList();

        var registerMethodNames = ExtractMethodNames(methodSymbols, className)
            .Concat(ExtractFieldNames(fieldSymbols, className))
            .Concat(ExtractPropertyNames(propertySymbols, className))
            .Distinct(StringComparer.Ordinal)
            .OrderBy(static name => name, StringComparer.Ordinal)
            .ToArray();

        if (registerMethodNames is [])
        {
            return null;
        }

        var fullyQualifiedClassName = classSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        var registerStatements = string.Join("\n", registerMethodNames.Select(static name => $"\t\t{name};"));
        return new RegisterClassData(fullyQualifiedClassName, registerStatements);
    }

    private static void GenerateFromData(
        SourceProductionContext spc,
        (ImmutableArray<MelonModClassData> MelonModClasses, ImmutableArray<RegisterClassData> RegisterClasses) data)
    {
        if (data.MelonModClasses.Length != 1 || data.RegisterClasses.IsDefaultOrEmpty)
        {
            return;
        }

        var melonModClass = data.MelonModClasses[0];
        var usingStringBuilder = new StringBuilder();
        var methodStringBuilder = new StringBuilder();
        var nameList = new HashSet<string>();
        foreach (var registerClass in data.RegisterClasses.OrderBy(static item => item.FullyQualifiedClassName, StringComparer.Ordinal))
        {
            if (!nameList.Add(registerClass.FullyQualifiedClassName))
            {
                continue;
            }

            usingStringBuilder.AppendLine($"using static {registerClass.FullyQualifiedClassName};");
            methodStringBuilder.AppendLine(registerClass.RegisterStatements);
        }

        var namespaceDeclaration = string.IsNullOrEmpty(melonModClass.Namespace)
            ? string.Empty
            : $"namespace {melonModClass.Namespace};\n\n";

        spc.AddSource($"{melonModClass.ClassName}.RegisterEntry.g.cs",
            Header +
            $$"""
              {{usingStringBuilder.ToString().TrimEnd()}}

              {{namespaceDeclaration}}partial class {{melonModClass.ClassName}}
              {
                  {{GetGeneratedCodeAttribute(typeof(RegisterEntryGenerator))}}
                  static {{melonModClass.ClassName}}()
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

    private sealed record MelonModClassData(string Namespace, string ClassName);

    private sealed record RegisterClassData(string FullyQualifiedClassName, string RegisterStatements);
}