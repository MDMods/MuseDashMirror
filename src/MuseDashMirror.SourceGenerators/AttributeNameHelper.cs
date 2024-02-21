namespace MuseDashMirror.SourceGenerators;

internal static class AttributeNameHelper
{
    internal const string LoggerAttributeName = "MuseDashMirror.Attributes.LoggerAttribute";
    internal const string RegisterInMuseDashMirrorAttributeName = "MuseDashMirror.Attributes.RegisterInMuseDashMirrorAttribute";
    internal const string PnlMenuEventAttributeName = "MuseDashMirror.Attributes.EventAttributes.PnlMenuEventAttribute";
    internal const string PnlStageEventAttributeName = "MuseDashMirror.Attributes.EventAttributes.PnlStageEventAttribute";
    internal const string MenuSelectEventAttributeName = "MuseDashMirror.Attributes.EventAttributes.MenuSelectEventAttribute";

    internal static readonly Regex SceneEventRegex =
        new(@"MuseDashMirror\.Attributes\.EventAttributes\.SceneEvents\.(.+Scene)Attribute", RegexOptions.Compiled);

    internal static readonly Regex PatchEventRegex =
        new(@"MuseDashMirror\.Attributes\.EventAttributes\.PatchEvents\.(.+Patch)Attribute", RegexOptions.Compiled);
}