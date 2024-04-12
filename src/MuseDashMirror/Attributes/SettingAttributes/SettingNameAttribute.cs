namespace MuseDashMirror.Attributes.SettingAttributes;

/// <summary>
///     Name of the setting shown in the UI
/// </summary>
/// <param name="settingName">Setting Name</param>
[AttributeUsage(AttributeTargets.Class)]
public sealed class SettingNameAttribute(string settingName) : Attribute;