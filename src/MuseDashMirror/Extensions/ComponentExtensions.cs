namespace MuseDashMirror.Extensions;

/// <summary>
///     <see cref="Component" /> Extension Methods
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
        if (!component.TryGetComponent<Text>(out var textComponent))
        {
            Logger.Error($"Component {component} does not have a Text component");
            return;
        }

        textComponent.text = text;
    }
}