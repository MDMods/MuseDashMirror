namespace MuseDashMirror.Extensions;

/// <summary>
///     <see cref="GameObject" /> Extension Methods
/// </summary>
[Logger]
public static partial class GameObjectExtensions
{
    /// <summary>
    ///     Set the parent of a GameObject
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="parent">Parent</param>
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
    /// <typeparam name="T">Component</typeparam>
    /// <returns>Component</returns>
    public static T FindComponentInAncestors<T>(this GameObject gameObject) where T : Component
    {
        while (true)
        {
            var component = gameObject.GetComponent<T>();
            if (component != null)
            {
                return component;
            }

            var parent = gameObject.transform.parent;
            if (parent == null)
            {
                Logger.Error($"{typeof(T)} not found in ancestors");
                return null;
            }

            gameObject = parent.gameObject;
        }
    }

    /// <summary>
    ///     Try to find a Component in the ancestors of a GameObject including itself
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <param name="component">Component</param>
    /// <typeparam name="T">Component</typeparam>
    /// <returns>Found</returns>
    public static bool TryFindComponentInAncestors<T>(this GameObject gameObject, out T component) where T : Component
    {
        while (true)
        {
            component = gameObject.GetComponent<T>();
            if (component != null)
            {
                return true;
            }

            var parent = gameObject.transform.parent;
            if (parent == null)
            {
                Logger.Error($"{typeof(T)} not found in ancestors");
                return false;
            }

            gameObject = parent.gameObject;
        }
    }

    /// <summary>
    ///     Get the total scale factor of a GameObject
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <returns>Scale Factor Vector3</returns>
    public static Vector3 GetTotalScaleFactor(this GameObject gameObject)
    {
        var scaleFactor = Vector3.one;
        while (true)
        {
            var transform = gameObject.transform;
            var localScale = transform.localScale;

            scaleFactor.x *= localScale.x;
            scaleFactor.y *= localScale.y;
            scaleFactor.z *= localScale.z;

            var parent = transform.parent;
            if (parent == null)
            {
                return scaleFactor;
            }

            gameObject = parent.gameObject;
        }
    }

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