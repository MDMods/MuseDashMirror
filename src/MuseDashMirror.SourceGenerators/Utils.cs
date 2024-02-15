namespace MuseDashMirror.SourceGenerators;

internal static class Utils
{
    internal static LocalizableString GetLocalizableString(string name) =>
        new LocalizableResourceString(name, ResourceManager, typeof(Resources));
}