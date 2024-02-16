using System.Reflection;

namespace MuseDashMirror.Attributes.EventAttributes;

[AttributeUsage(AttributeTargets.Method)]
public sealed class ExitWelcomeSceneAttribute : Attribute, IEventAttribute
{
    public void Register(MethodInfo method)
    {
        OnExitWelcomeScene += (EventHandler<SceneEventArgs>)Delegate.CreateDelegate(typeof(EventHandler<SceneEventArgs>), method);
    }
}