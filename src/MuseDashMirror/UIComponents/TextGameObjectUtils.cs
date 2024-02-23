namespace MuseDashMirror.UIComponents;

/// <summary>
///     Methods for creating text GameObject
/// </summary>
public static class TextGameObjectUtils
{
    /// <summary>
    ///     Create a ellipse text GameObject
    /// </summary>
    /// <param name="name">GameObject Name</param>
    /// <param name="parentName">Parent GameObject Name</param>
    /// <param name="ellipseTextParameters">Ellipse Text Parameters</param>
    /// <param name="transformParameters">Transform Parameters</param>
    /// <returns></returns>
    public static GameObject CreateEllipseText(string name, string parentName,
        EllipseTextParameters ellipseTextParameters, TransformParameters transformParameters)
    {
        var parent = GetGameObject(parentName);
        return CreateEllipseText(name, parent, ellipseTextParameters, transformParameters);
    }

    /// <summary>
    ///     Create an ellipse text GameObject
    /// </summary>
    /// <param name="name">GameObject Name</param>
    /// <param name="parent">Parent GameObject</param>
    /// <param name="ellipseTextParameters">Ellipse Text Parameters</param>
    /// <param name="transformParameters">Transform Parameters</param>
    /// <returns></returns>
    public static GameObject CreateEllipseText(string name, GameObject parent,
        EllipseTextParameters ellipseTextParameters, TransformParameters transformParameters)
    {
        var gameObject = new GameObject(name);
        gameObject.SetParent(parent);
        gameObject.SetTextComponent(ellipseTextParameters);
        gameObject.SetRectTransform(transformParameters);

        GameObjectCache[name] = gameObject;
        return gameObject;
    }

    /// <summary>
    ///     Create a text GameObject
    /// </summary>
    /// <param name="name">GameObject Name</param>
    /// <param name="parentName">Parent GameObject Name</param>
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
    /// <param name="parent">Parent GameObject</param>
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