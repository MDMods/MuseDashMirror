namespace MuseDashMirror.Attributes;

/// <summary>
///     Attribute for creating a toggle in the PnlMenu
/// </summary>
/// <param name="name">Toggle GameObject Name</param>
/// <param name="text">Toggle Text</param>
/// <param name="fullBoolName">Bounded Boolean FullName</param>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class PnlMenuToggleAttribute(string name, string text, string fullBoolName) : Attribute;