namespace MuseDashMirror.Attributes;

/// <summary>
///     <para>Create a <see cref="MelonLoader.MelonLogger"/> with the name of the class</para>
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
public sealed class LoggerAttribute(LoggerType type = LoggerType.StaticReadonly) : Attribute;