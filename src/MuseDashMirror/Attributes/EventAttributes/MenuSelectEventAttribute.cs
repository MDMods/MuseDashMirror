using System.Reflection;

namespace MuseDashMirror.Attributes.EventAttributes;

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
public class MenuSelectEventAttribute : Attribute, IEventAttribute
{
    /// <summary>
    ///     Register the method to <see cref="MenuSelectEvent" />
    /// </summary>
    /// <param name="method"></param>
    public void Register(MethodInfo method)
    {
        MenuSelectEvent += (EventHandler<MenuSelectEventArgs>)Delegate.CreateDelegate(typeof(EventHandler<MenuSelectEventArgs>), method);
    }
}