using System.Reflection;

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
public class PnlMenuEventAttribute : Attribute, IEventAttribute
{
    /// <summary>
    ///     Register the method to <see cref="PnlMenuEvent" />
    /// </summary>
    /// <param name="method"></param>
    public void Register(MethodInfo method)
    {
        PnlMenuEvent += (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), method);
    }
}