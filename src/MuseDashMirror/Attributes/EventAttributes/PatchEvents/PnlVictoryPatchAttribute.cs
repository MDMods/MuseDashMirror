namespace MuseDashMirror.Attributes.EventAttributes.PatchEvents;

/// <summary>
///     <para>
///         Add this attribute to a method to make it run after
///         <see
///             cref="PnlVictory.OnVictory(Il2CppSystem.Object,Il2CppSystem.Object,Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray{Il2CppSystem.Object})" />
///         <br />
///         Method can be any accessibility level but must be static
///     </para>
///     <example>
///         The method must have the following signature:
///         <code>
///         private static void MethodName(object sender, PnlVictoryEventArgs e)
///         </code>
///     </example>
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class PnlVictoryPatchAttribute : Attribute;