namespace MuseDashMirror.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class RegisterInMuseDashMirrorAttribute(RegisterMethodType registerMethodType = RegisterMethodType.StaticConstructor) : Attribute;