namespace MuseDashMirror.Extensions;

/// <summary>
///     GameObject Extension Methods
/// </summary>
[Logger]
public static partial class GameObjectExtensions
{
    /// <summary>
    ///     Set the parent of a GameObject
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="parent">Parent</param>
    public static void SetParent(this GameObject gameObject, GameObject parent) => gameObject.transform.SetParent(parent.transform);

    /// <summary>
    ///     Set the text of a GameObject with a Text component
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="text">Text</param>
    public static void SetText(this GameObject gameObject, string text)
    {
        if (gameObject.GetComponent<Text>() == null)
        {
            Logger.Error($"GameObject {gameObject} does not have a Text component");
            return;
        }

        gameObject.GetComponent<Text>().text = text;
    }
}