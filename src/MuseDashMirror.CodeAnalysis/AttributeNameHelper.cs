namespace MuseDashMirror.CodeAnalysis;

internal static class AttributeNameHelper
{
    private const string SceneEventAttributeNamespace = "MuseDashMirror.Attributes.EventAttributes.SceneEvents";
    private const string PatchEventAttributeNamespace = "MuseDashMirror.Attributes.EventAttributes.PatchEvents";

    internal const string LoggerAttributeName = "MuseDashMirror.Attributes.LoggerAttribute";
    internal const string MelonInfoAttributeName = "MelonLoader.MelonInfoAttribute";
    internal const string PnlMenuToggleAttributeName = "MuseDashMirror.Attributes.PnlMenuToggleAttribute";

    internal static string? GetSceneEventName(INamedTypeSymbol? attributeClass) => GetEventName(attributeClass, SceneEventAttributeNamespace);
    internal static string? GetPatchEventName(INamedTypeSymbol? attributeClass) => GetEventName(attributeClass, PatchEventAttributeNamespace);

    private static string? GetEventName(INamedTypeSymbol? attributeClass, string eventNamespace) =>
        attributeClass?.ContainingNamespace.ToDisplayString() == eventNamespace
            ? attributeClass.Name[..^9]
            : null;
}
