namespace MuseDashMirror.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class PnlMenuToggleAttribute(string name, string text, string fullBoolName) : Attribute;