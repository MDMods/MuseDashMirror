namespace MuseDashMirror.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class RegisterEventsAttribute(ConstructorType constructorType) : Attribute
{
    public RegisterEventsAttribute() : this(ConstructorType.Static)
    {
    }
}