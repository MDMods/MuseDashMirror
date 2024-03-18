namespace MuseDashMirror.Extensions.UnityExtensions;

/// <summary>
///     <see cref="GameObject" /> Extension Methods
/// </summary>
[Logger]
public static partial class GameObjectExtensions
{
    /// <summary>
    ///     Get the Parent GameObject of a GameObject
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <returns>Parent GameObject</returns>
    public static GameObject GetParentGameObject(this GameObject gameObject) => gameObject.GetParentTransform().gameObject;

    /// <summary>
    ///     Get the Parent Transform of a GameObject
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <returns>Parent Transform</returns>
    public static Transform GetParentTransform(this GameObject gameObject) => gameObject.transform.parent;

    /// <summary>
    ///     Get the Child <paramref name="gameObject" /> at the specified <paramref name="indexes" />
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="indexes">Indexes</param>
    /// <returns>Child GameObject</returns>
    public static GameObject GetChildGameObject(this GameObject gameObject, params int[] indexes) => gameObject.transform.GetChild(indexes).gameObject;

    /// <summary>
    ///     Set the Parent of a GameObject
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="parent">Parent GameObject</param>
    /// <param name="worldPositionStays">World Position Stays</param>
    public static void SetParent(this GameObject gameObject, GameObject parent, bool worldPositionStays = true)
        => gameObject.transform.SetParent(parent.transform, worldPositionStays);

    /// <summary>
    ///     Set the text of a GameObject with a Text gameObject
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="text">Text</param>
    public static void SetText(this GameObject gameObject, string text)
    {
        if (!gameObject.TryGetComponent<Text>(out var textComponent))
        {
            Logger.Error($"GameObject {gameObject} does not have a Text component");
            return;
        }

        textComponent.text = text;
    }

    /// <summary>
    ///     Set the color of a GameObject with a Text gameObject
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="color">Color</param>
    public static void SetColor(this GameObject gameObject, Color color)
    {
        if (!gameObject.TryGetComponent<Text>(out var textComponent))
        {
            Logger.Error($"GameObject {gameObject} does not have a Text component");
            return;
        }

        textComponent.color = color;
    }

    /// <summary>
    ///     Set the Text Component of a GameObject using Text Parameters
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="textParameters">Text Parameters</param>
    public static void SetTextComponent(this GameObject gameObject, TextParameters textParameters)
    {
        var textComponent = gameObject.GetComponent<Text>() ?? gameObject.AddComponent<Text>();
        textComponent.text = textParameters.GetText();
        textComponent.font = textParameters.Font;
        textComponent.fontSize = textParameters.FontSize;
        textComponent.color = textParameters.Color;
        textComponent.alignment = textParameters.Alignment;
    }

    /// <summary>
    ///     Set the RectTransform of a GameObject using Transform Parameters
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="transformParameters">Transform Parameters</param>
    public static void SetRectTransform(this GameObject gameObject, TransformParameters transformParameters)
    {
        var rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.localScale = transformParameters.LocalScale;

        if (transformParameters.IsAutoSize)
        {
            gameObject.AddContentSizeFitter();
        }
        else
        {
            rectTransform.sizeDelta = transformParameters.SizeDelta;
        }

        rectTransform.UpdateTransformLayoutInfo();
        transformParameters.PositionStrategy.SetPosition(rectTransform, transformParameters);
    }

    /// <summary>
    ///     Find a Component in the ancestors of a GameObject including itself
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="includeSelf">Include Self</param>
    /// <typeparam name="T">Component</typeparam>
    /// <returns>Component</returns>
    public static T FindComponentInAncestors<T>(this GameObject gameObject, bool includeSelf = true) where T : Component
    {
        var component = includeSelf ? gameObject.GetComponent<T>() : null;
        var parentTransform = gameObject.GetParentTransform();

        while (parentTransform != null)
        {
            if (component != null)
            {
                return component;
            }

            component = parentTransform.gameObject.GetComponent<T>();
            parentTransform = parentTransform.parent;
        }

        return component;
    }

    /// <summary>
    ///     Try to find a Component in the ancestors of a GameObject including itself
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="component">Component</param>
    /// <param name="includeSelf">Include Self</param>
    /// <typeparam name="T">Component</typeparam>
    /// <returns>Found</returns>
    public static bool TryFindComponentInAncestors<T>(this GameObject gameObject, out T component, bool includeSelf = true) where T : Component
    {
        component = includeSelf ? gameObject.GetComponent<T>() : null;
        var parentTransform = gameObject.GetParentTransform();

        while (parentTransform != null)
        {
            if (component != null)
            {
                return true;
            }

            component = parentTransform.gameObject.GetComponent<T>();
            parentTransform = parentTransform.parent;
        }

        return false;
    }

    /// <summary>
    ///     Get the total scale factor of a GameObject
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="includeSelf">Include Self</param>
    /// <returns>Scale Factor Vector3</returns>
    public static Vector3 GetTotalScaleFactor(this GameObject gameObject, bool includeSelf = true)
    {
        var scaleFactor = includeSelf ? gameObject.transform.localScale : Vector3.one;
        var parentTransform = gameObject.GetParentTransform();

        while (parentTransform != null)
        {
            scaleFactor = Vector3.Scale(scaleFactor, parentTransform.localScale);
            parentTransform = parentTransform.parent;
        }

        return scaleFactor;
    }

    /// <summary>
    ///     Get the Canvas Scaler Factor of a GameObject
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <returns>Canvas Scaler Factor</returns>
    public static float GetCanvasScalerFactor(this GameObject gameObject)
        => gameObject.TryFindComponentInAncestors(out CanvasScaler canvasScaler) ? canvasScaler.referenceResolution.x / Screen.width : 1f;

    /// <summary>
    ///     Add a ContentSizeFitter to a GameObject
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    public static void AddContentSizeFitter(this GameObject gameObject)
    {
        var contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }
}