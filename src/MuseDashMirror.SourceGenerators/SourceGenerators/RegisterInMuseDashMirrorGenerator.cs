namespace MuseDashMirror.SourceGenerators;

[Generator(LanguageNames.CSharp)]
public sealed class RegisterInMuseDashMirrorGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(
            context.SyntaxProvider.ForAttributeWithMetadataName(RegisterInMuseDashMirrorAttributeName,
                FilterNode, ExtractDataFromContext),
            GenerateFromData);
    }

    private static bool FilterNode(SyntaxNode node, CancellationToken _) =>
        node is ClassDeclarationSyntax { Modifiers: var modifiers and not [] } && modifiers.Any(SyntaxKind.PartialKeyword);

    private static RegisterInMuseDashMirrorData? ExtractDataFromContext(GeneratorAttributeSyntaxContext ctx, CancellationToken _)
    {
        if (ctx is not
            {
                TargetNode: ClassDeclarationSyntax classDeclaration,
                TargetSymbol: INamedTypeSymbol { Name: var className, ContainingNamespace: var @namespace },
                Attributes: var attributes
            })
        {
            return null;
        }

        var attribute = attributes.First(x => x.AttributeClass!.ToDisplayString() == RegisterInMuseDashMirrorAttributeName);
        var registerMethodType = (RegisterMethodType)attribute.ConstructorArguments.Single().Value!;

        var classSymbol = ctx.SemanticModel.GetDeclaredSymbol(classDeclaration);
        var methodSymbols = classSymbol?.GetMembers().OfType<IMethodSymbol>();

        var methodDataList = (
            from method in methodSymbols!
            let methodName = method.Name
            let methodAttributes = method.GetAttributes()
            let sceneEventNames = methodAttributes
                .Select(static attribute => SceneEventRegex.Match(attribute.AttributeClass!.ToDisplayString()))
                .Where(static match => match.Success)
                .Select(static match => match.Groups[1].Value)
                .ToList()
            let patchEventNames = methodAttributes
                .Select(static attribute => PatchEventRegex.Match(attribute.AttributeClass!.ToDisplayString()))
                .Where(static match => match.Success)
                .Select(static match => match.Groups[1].Value)
                .ToList()
            select new MethodData(methodName, sceneEventNames, patchEventNames)).ToList();

        return new RegisterInMuseDashMirrorData(@namespace.ToDisplayString(), className, registerMethodType, methodDataList);
    }

    private static void GenerateFromData(SourceProductionContext spc, RegisterInMuseDashMirrorData? data)
    {
        if (data is not var (@namespace, className, registerMethodType, methodDataList))
        {
            return;
        }

        var constructorName = registerMethodType switch
        {
            RegisterMethodType.Constructor => $"public {className}()",
            RegisterMethodType.StaticConstructor => $"static {className}()",
            RegisterMethodType.Method => $"internal static void Register{className}()",
            _ => throw new ArgumentOutOfRangeException(nameof(registerMethodType), registerMethodType, null)
        };

        var constructorContent = GetConstructorContent(methodDataList);

        spc.AddSource($"{className}_Register.g.cs",
            Header +
            $$"""
              namespace {{@namespace}};

              partial class {{className}}
              {
                  {{GetGeneratedCodeAttribute(nameof(RegisterInMuseDashMirrorGenerator))}}
                  {{constructorName}}
                  {
              {{constructorContent}}
                  }
              }
              """);
    }

    private static string GetConstructorContent(List<MethodData> methodDataList)
    {
        var constructorContentStringBuilder = new StringBuilder();
        foreach (var methodData in methodDataList)
        {
            foreach (var sceneEventName in methodData.SceneEventNames)
            {
                constructorContentStringBuilder.AppendLine($"\t\tRegister{methodData.Name}To{sceneEventName}Event();");
            }

            foreach (var patchEventName in methodData.PatchEventNames)
            {
                constructorContentStringBuilder.AppendLine($"\t\tRegister{methodData.Name}To{patchEventName}Event();");
            }
        }

        return constructorContentStringBuilder.ToString().TrimEnd();
    }

    private sealed record RegisterInMuseDashMirrorData(
        string Namespace,
        string ClassName,
        RegisterMethodType RegisterMethodType,
        List<MethodData> MethodDataList);

    private sealed record MethodData(string Name, List<string> SceneEventNames, List<string> PatchEventNames);
}