namespace MuseDashMirror.Extensions.UnityExtensions;

/// <summary>
///     <see cref="GameObject" /> Extension Methods
/// </summary>
[Logger]
public static partial class GameObjectExtensions
{
    /// <param name="gameObject">GameObject</param>
    extension(GameObject gameObject)
    {
        /// <summary>
        ///     Get the Parent GameObject of a GameObject
        /// </summary>
        /// <returns>Parent GameObject</returns>
        public GameObject GetParentGameObject() => gameObject.GetParentTransform().gameObject;

        /// <summary>
        ///     Get the Parent Transform of a GameObject
        /// </summary>
        /// <returns>Parent Transform</returns>
        public Transform GetParentTransform() => gameObject.transform.parent;

        /// <summary>
        ///     Set the Parent of a GameObject
        /// </summary>
        /// <param name="parent">Parent GameObject</param>
        /// <param name="worldPositionStays">World Position Stays</param>
        public void SetParent(GameObject parent, bool worldPositionStays = true)
            => gameObject.transform.SetParent(parent.transform, worldPositionStays);

        /// <summary>
        ///     Set the text of a GameObject with a Text gameObject
        /// </summary>
        /// <param name="text">Text</param>
        public void SetText(string text)
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
        /// <param name="color">Color</param>
        public void SetColor(Color color)
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
        /// <param name="textParameters">Text Parameters</param>
        public void SetTextComponent(TextParameters textParameters)
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
        /// <param name="transformParameters">Transform Parameters</param>
        public void SetRectTransform(TransformParameters transformParameters)
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
        /// <param name="includeSelf">Include Self</param>
        /// <typeparam name="T">Component</typeparam>
        /// <returns>Component</returns>
        public T FindComponentInAncestors<T>(bool includeSelf = true) where T : Component
        {
            var currentTransform = includeSelf ? gameObject.transform : gameObject.GetParentTransform();

            while (currentTransform != null)
            {
                var component = currentTransform.gameObject.GetComponent<T>();
                if (component != null)
                {
                    return component;
                }

                currentTransform = currentTransform.parent;
            }

            return null;
        }

        /// <summary>
        ///     Try to find a Component in the ancestors of a GameObject including itself
        /// </summary>
        /// <param name="component">Component</param>
        /// <param name="includeSelf">Include Self</param>
        /// <typeparam name="T">Component</typeparam>
        /// <returns>Found</returns>
        public bool TryFindComponentInAncestors<T>(out T component, bool includeSelf = true) where T : Component
        {
            var currentTransform = includeSelf ? gameObject.transform : gameObject.GetParentTransform();

            while (currentTransform != null)
            {
                component = currentTransform.gameObject.GetComponent<T>();
                if (component != null)
                {
                    return true;
                }

                currentTransform = currentTransform.parent;
            }

            component = null;
            return false;
        }

        /// <summary>
        ///     Get the total scale factor of a GameObject
        /// </summary>
        /// <param name="includeSelf">Include Self</param>
        /// <returns>Scale Factor Vector3</returns>
        public Vector3 GetTotalScaleFactor(bool includeSelf = true)
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
        /// <returns>Canvas Scaler Factor</returns>
        public float GetCanvasScalerFactor()
            => gameObject.TryFindComponentInAncestors(out CanvasScaler canvasScaler) ? canvasScaler.referenceResolution.x / Screen.width : 1f;

        /// <summary>
        ///     Add a ContentSizeFitter to a GameObject
        /// </summary>
        public void AddContentSizeFitter()
        {
            var contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }
}
