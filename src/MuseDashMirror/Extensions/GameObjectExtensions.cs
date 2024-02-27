using UnityEngine.UI;
using Object = UnityEngine.Object;

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
    ///     Set the text of a GameObject with a Text gameObject
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

    /// <summary>
    ///     Set the Text gameObject of a GameObject
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
    ///     Set the RectTransform of a GameObject
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
    ///     Find a Component in the ancestors of a GameObject
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
    ///     Get the total scaled factor of a GameObject
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    /// <returns>Scaled Factor</returns>
    public static float GetTotalScaledFactor(this GameObject gameObject)
    {
        var scaleFactor = 1f;
        while (true)
        {
            scaleFactor *= gameObject.transform.localScale.x;

            var parent = gameObject.transform.parent;
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

    /// <summary>
    ///     Destroy a GameObject
    /// </summary>
    /// <param name="gameObject">GameObject</param>
    public static void Destroy(this GameObject gameObject) => Object.Destroy(gameObject);
}