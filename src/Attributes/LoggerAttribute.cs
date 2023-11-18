namespace MuseDashMirror.Attributes;

/// <summary>
///     <para>Set a field or property as a MelonLogger with the name of the class it is in</para>
///     <para>
///         The field or property can be any accessibility level but has to be static and cannot be readonly<br />
///         The type should be <see cref="MelonLoader.MelonLogger.Instance" />
///     </para>
///     <example>
///         <code>
///         [Logger] private static MelonLogger.Instance Logger;
///         </code>
///         <code>
///         [Logger] private static MelonLogger.Instance Logger { get; set; }
///         </code>
///     </example>
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class LoggerAttribute : Attribute;