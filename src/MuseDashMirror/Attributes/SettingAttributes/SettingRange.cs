namespace MuseDashMirror.Attributes.SettingAttributes;

/// <summary>
///     Range of the setting
/// </summary>
/// <param name="min">Min Value</param>
/// <param name="max">Max Value</param>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class SettingRangeAttribute(int min, int max) : Attribute;