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
/// <param name="type">Logger Type</param>
[AttributeUsage(AttributeTargets.Class)]
public sealed class LoggerAttribute(LoggerType type) : Attribute
{
    public LoggerAttribute() : this(LoggerType.Readonly)
    {
    }
}