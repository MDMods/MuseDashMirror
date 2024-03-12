namespace MuseDashMirror.SourceGenerators;

[Generator]
public sealed class PnlMenuToggleGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(
            context.SyntaxProvider.ForAttributeWithMetadataName(
                PnlMenuToggleAttributeName, FilterNode, ExtractDataFromContext),
            GenerateFromData);
    }

    private static bool FilterNode(SyntaxNode node, CancellationToken _) => node switch
    {
        PropertyDeclarationSyntax
        {
            Type: IdentifierNameSyntax { Identifier.ValueText: "GameObject" },
            Parent: ClassDeclarationSyntax { Modifiers: var modifiers and not [] }
        } when modifiers.Any(SyntaxKind.PartialKeyword) => true,

        VariableDeclaratorSyntax
        {
            Parent: VariableDeclarationSyntax
            {
                Type: IdentifierNameSyntax { Identifier.ValueText: "GameObject" },
                Parent.Parent: ClassDeclarationSyntax { Modifiers: var modifiers and not [] }
            }
        } when modifiers.Any(SyntaxKind.PartialKeyword) => true,

        _ => false
    };

    private static PnlMenuToggleData? ExtractDataFromContext(GeneratorAttributeSyntaxContext ctx, CancellationToken _)
    {
        if (ctx.TargetSymbol is not (IPropertySymbol or IFieldSymbol))
        {
            return null;
        }

        if (!ctx.TargetSymbol.IsStatic)
        {
            return null;
        }

        var unitRoot = ctx.SemanticModel.SyntaxTree.GetCompilationUnitRoot();

        var staticUsingDirectives = from usingDirective in unitRoot.Usings
            where usingDirective.StaticKeyword.IsKind(SyntaxKind.StaticKeyword)
            select usingDirective.ToFullString();

        var className = GetClassName(ctx.TargetNode);

        var @namespace = ctx.TargetSymbol.ContainingNamespace.ToDisplayString();
        var variableName = ctx.TargetSymbol.Name;
        var attribute = ctx.TargetSymbol.GetAttributes().First(x => x.AttributeClass!.ToDisplayString() == PnlMenuToggleAttributeName);

        if (attribute.ApplicationSyntaxReference!.GetSyntax() is not AttributeSyntax { ArgumentList.Arguments: var arguments })
        {
            return null;
        }

        var boolName = arguments[2].Expression.ToString().Contains("nameof(")
            ? arguments[2].Expression.ToString()[7..^1]
            : arguments[2].Expression.ToString();

        var constructorArguments = attribute.ConstructorArguments.Select(x => x.Value!.ToString()).Take(2).ToArray();

        return new PnlMenuToggleData(staticUsingDirectives, @namespace, className, variableName, constructorArguments[0], constructorArguments[1],
            boolName);
    }

    private static void GenerateFromData(SourceProductionContext spc, PnlMenuToggleData? data)
    {
        if (data is not var (staticUsingDirectives, @namespace, className, variableName, toggleName, toggleText, boolName))
        {
            return;
        }

        using var usingStringBuilder = ZString.CreateStringBuilder(true);
        foreach (var usingDirective in staticUsingDirectives)
        {
            usingStringBuilder.AppendLine(usingDirective.Insert(13, "global::"));
        }

        spc.AddSource($"{className}.{variableName}.PnlMenuToggle.g.cs",
            Header +
            $$"""
              using global::MuseDashMirror.EventArguments;
              using static global::MuseDashMirror.PatchEvents;
              using static global::MuseDashMirror.UIComponents.ToggleUtils;
              {{usingStringBuilder.ToString().TrimEnd()}}

              namespace {{@namespace}};

              partial class {{className}}
              {
                  {{GetGeneratedCodeAttribute(nameof(PnlMenuToggleGenerator))}}
                  internal static void Register{{className}}{{variableName}}ToPnlMenuEvent() =>
                      PnlMenuPatch += (_,_) => {{variableName}} = CreatePnlMenuToggle("{{toggleName}}", "{{toggleText}}", {{boolName}}, new Action<bool>(val => {{boolName}} = val));
              }
              """);
    }

    private static string GetClassName(SyntaxNode node) => node switch
    {
        PropertyDeclarationSyntax { Parent: ClassDeclarationSyntax { Identifier.ValueText: var className } } => className,
        VariableDeclaratorSyntax { Parent.Parent.Parent: ClassDeclarationSyntax { Identifier.ValueText: var className } } => className,
        _ => string.Empty
    };

    private sealed record PnlMenuToggleData(
        IEnumerable<string> Usings,
        string Namespace,
        string ClassName,
        string VariableName,
        string ToggleName,
        string ToggleText,
        string BoolName);
}