namespace MuseDashMirror.Attributes.EventAttributes.SceneEvents;

/// <summary>
///     <para>
///         Add this attribute to a method to make it run when exiting <b>Game Scene</b><br />
///         Method can be any accessibility level
///     </para>
///     <example>
///         The method must have the following signature:
///         <code>
///         private static void MethodName(object sender, SceneEventArgs e)
///         </code>
///     </example>
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class ExitGameSceneAttribute : Attribute;