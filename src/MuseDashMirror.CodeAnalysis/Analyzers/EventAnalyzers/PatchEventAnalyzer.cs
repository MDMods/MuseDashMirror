namespace MuseDashMirror.CodeAnalysis.Analyzers.EventAnalyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PatchEventAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [PatchEventAttributeInvalidArgsError];

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

        var patchEventName = context.ContainingSymbol!.GetAttributes()
            .Select(static attribute => GetPatchEventName(attribute.AttributeClass))
            .FirstOrDefault(static eventName => eventName is not null);

        if (patchEventName is null)
        {
            return;
        }

        var desiredParameterType = $"{patchEventName[..^5]}EventArgs";

        if (parameters.Count != 2 || parameters[1].Type is not IdentifierNameSyntax { Identifier.ValueText: var parameterType } ||
            parameterType != desiredParameterType)
        {
            context.ReportDiagnostic(Diagnostic.Create(PatchEventAttributeInvalidArgsError, methodDeclaration.Identifier.GetLocation(),
                context.ContainingSymbol.Name, patchEventName, desiredParameterType));
        }
    }
}
