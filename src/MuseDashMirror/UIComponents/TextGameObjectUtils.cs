namespace MuseDashMirror.UIComponents;

/// <summary>
///     Methods for creating text GameObject
/// </summary>
public static class TextGameObjectUtils
{
    /// <summary>
    ///     Create a text GameObject
    /// </summary>
    /// <param name="name">GameObject Name</param>
    /// <param name="parentName">GameObject Parent Name</param>
    /// <param name="textParameters">Text Parameters</param>
    /// <param name="transformParameters">Transform Parameters</param>
    /// <returns>Text GameObject</returns>
    public static GameObject CreateText(string name, string parentName, TextParameters textParameters, TransformParameters transformParameters)
    {
        var parent = GetGameObject(parentName);
        return CreateText(name, parent, textParameters, transformParameters);
    }

    /// <summary>
    ///     Create a text GameObject
    /// </summary>
    /// <param name="name">GameObject Name</param>
    /// <param name="parent">GameObject Parent</param>
    /// <param name="textParameters">Text Parameters</param>
    /// <param name="transformParameters">Transform Parameters</param>
    /// <returns>Text GameObject</returns>
    public static GameObject CreateText(string name, GameObject parent, TextParameters textParameters, TransformParameters transformParameters)
    {
        var gameObject = new GameObject(name);
        gameObject.SetParent(parent);
        gameObject.SetTextComponent(textParameters);
        gameObject.SetRectTransform(transformParameters);

        GameObjectCache[name] = gameObject;
        return gameObject;
    }
}