namespace MuseDashMirror.SourceGenerators;

internal static class AttributeNameHelper
{
    internal const string LoggerAttributeName = "MuseDashMirror.Attributes.LoggerAttribute";
    internal const string PnlMenuToggleAttributeName = "MuseDashMirror.Attributes.PnlMenuToggleAttribute";
    internal const string SettingNameAttributeName = "MuseDashMirror.Attributes.SettingAttributes.SettingNameAttribute";
    internal const string MelonInfoAttributeName = "MelonLoader.MelonInfoAttribute";

    internal static readonly Regex SceneEventRegex =
        new(@"MuseDashMirror\.Attributes\.EventAttributes\.SceneEvents\.(.+Scene)Attribute", RegexOptions.Compiled);

    internal static readonly Regex PatchEventRegex =
        new(@"MuseDashMirror\.Attributes\.EventAttributes\.PatchEvents\.(.+Patch)Attribute", RegexOptions.Compiled);
}