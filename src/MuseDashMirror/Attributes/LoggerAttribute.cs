namespace MuseDashMirror.Attributes;

/// <summary>
///     <para>Create a MelonLogger with the name of the class</para>
///     <example>
///         <code>
///         [Logger]
///         internal partial class ExampleClass; 
///         </code>
///         <code>
///         [Logger(LoggerType.StaticReadonly)]
///         internal static partial class ExampleClass;
///         </code>
///     </example>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class LoggerAttribute(LoggerType type) : Attribute
{
    private LoggerType Type { get; set; } = type;

    public LoggerAttribute() : this(LoggerType.Readonly)
    {
    }
}