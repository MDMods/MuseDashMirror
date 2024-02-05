using System.Reflection;
using MuseDashMirror.EventArguments;

namespace MuseDashMirror.Attributes.EventAttributes;

/// <summary>
///     <para>
///         Add this attribute to a method to make it run when entering game scene<br />
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
public class EnterGameSceneAttribute : Attribute, IEventAttribute
{
    /// <summary>
    ///     Register the method to <see cref="OnEnterGameScene" />
    /// </summary>
    /// <param name="method"></param>
    public void Register(MethodInfo method)
    {
        OnEnterGameScene += (EventHandler<SceneEventArgs>)Delegate.CreateDelegate(typeof(EventHandler<SceneEventArgs>), method);
    }
}