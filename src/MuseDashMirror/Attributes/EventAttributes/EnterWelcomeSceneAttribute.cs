using System.Reflection;

namespace MuseDashMirror.Attributes.EventAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class EnterWelcomeSceneAttribute : Attribute, IEventAttribute
{
    public void Register(MethodInfo method)
    {
        OnEnterWelcomeScene += (EventHandler<SceneEventArgs>)Delegate.CreateDelegate(typeof(EventHandler<SceneEventArgs>), method);
    }
}