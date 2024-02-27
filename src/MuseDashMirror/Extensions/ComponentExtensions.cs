using Object = UnityEngine.Object;

namespace MuseDashMirror.Extensions;

/// <summary>
///     Component Extension Methods
/// </summary>
[Logger]
public static partial class ComponentExtensions
{
    /// <summary>
    ///     Set the text of a Component with a Text component
    /// </summary>
    /// <param name="component">GameObject Component</param>
    /// <param name="text">Text</param>
    public static void SetText(this Component component, string text)
    {
        if (component.GetComponent<Text>() == null)
        {
            Logger.Error($"Component {component} does not have a Text component");
            return;
        }

        component.GetComponent<Text>().text = text;
    }

    /// <summary>
    ///     Destroy a GameObject Component
    /// </summary>
    /// <param name="component">GameObject Component</param>
    public static void Destroy(this Component component) => Object.Destroy(component);
}