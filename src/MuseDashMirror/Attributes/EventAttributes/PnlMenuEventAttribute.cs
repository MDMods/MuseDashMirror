using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.Attributes.EventAttributes;

/// <summary>
///     <para>
///         Add this attribute to a method to make it run after <see cref="PnlMenu.Awake" /><br />
///         Method can be any accessibility level but must be static
///     </para>
///     <example>
///         The method must have the following signature:
///         <code>
///         private static void MethodName(object sender, EventArgs e)
///         </code>
///     </example>
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class PnlMenuEventAttribute : Attribute;