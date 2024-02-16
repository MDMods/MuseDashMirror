using System.Reflection;

namespace MuseDashMirror.Attributes.EventAttributes;

/// <summary>
///     <para>
///         Add this attribute to a method to make it run when exiting loading scene<br />
///         Method can be any accessibility level but must be static
///     </para>
///     <example>
///         The method must have the following signature:
///         <code>
///         private static void MethodName(object sender, SceneEventArgs e)
///         </code>
///     </example>
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class ExitLoadingSceneAttribute : Attribute, IEventAttribute
{
    /// <summary>
    ///     Register the method to <see cref="OnExitLoadingScene" />
    /// </summary>
    /// <param name="method"></param>
    public void Register(MethodInfo method)
    {
        OnExitLoadingScene += (EventHandler<SceneEventArgs>)Delegate.CreateDelegate(typeof(EventHandler<SceneEventArgs>), method);
    }
}