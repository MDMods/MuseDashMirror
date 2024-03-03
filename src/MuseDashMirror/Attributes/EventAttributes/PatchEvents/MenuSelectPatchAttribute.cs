namespace MuseDashMirror.Attributes.EventAttributes.PatchEvents;

/// <summary>
///     <para>
///         Add this attribute to a method to make it run after <see cref="MenuSelect.OnToggleChanged" /><br />
///         Method can be any accessibility level but must be static
///     </para>
///     <example>
///         The method must have the following signature:
///         <code>
///         private static void MethodName(object sender, MenuSelectEventArgs e)
///         </code>
///     </example>
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class MenuSelectPatchAttribute : Attribute;