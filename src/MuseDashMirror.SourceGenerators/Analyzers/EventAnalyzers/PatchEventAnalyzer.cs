namespace MuseDashMirror.SourceGenerators.Analyzers.EventAnalyzers;

[UsedImplicitly]
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PatchEventAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(PatchEventAttributeInvalidArgsError);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.MethodDeclaration);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not MethodDeclarationSyntax
            {
                ParameterList.Parameters: var parameters,
                Parent: ClassDeclarationSyntax
            } methodDeclaration)
        {
            return;
        }

        var attribute = context.ContainingSymbol!.GetAttributes().FirstOrDefault(x => PatchEventRegex.IsMatch(x.AttributeClass!.ToDisplayString()));
        if (attribute is null)
        {
            return;
        }

        var match = PatchEventRegex.Match(attribute.AttributeClass!.ToDisplayString());
        var patchEventName = match.Groups[1].Value;
        var desiredParameterType = $"{patchEventName[..^5]}EventArgs";

        if (parameters.Count != 2 || parameters[1].Type is not IdentifierNameSyntax { Identifier.ValueText: var parameterType } ||
            parameterType != desiredParameterType)
        {
            context.ReportDiagnostic(Diagnostic.Create(PatchEventAttributeInvalidArgsError, methodDeclaration.Identifier.GetLocation(),
                context.ContainingSymbol.Name, patchEventName, desiredParameterType));
        }
    }
}