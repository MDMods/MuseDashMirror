using System.Reflection;
using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.Attributes.EventAttributes;

/// <summary>
///     <para>
///         Add this attribute to a method to make it run after <see cref="PnlStage.Awake" /><br />
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
public class PnlStageEventAttribute : Attribute, IEventAttribute
{
    /// <summary>
    ///     Register method to <see cref="PnlStageEvent" />
    /// </summary>
    /// <param name="method"></param>
    public void Register(MethodInfo method)
    {
        PnlStageEvent += (EventHandler<PnlStageEventArgs>)Delegate.CreateDelegate(typeof(EventHandler<PnlStageEventArgs>), method);
    }
}