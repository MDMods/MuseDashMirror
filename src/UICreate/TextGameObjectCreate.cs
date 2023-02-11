using UnityEngine;
using UnityEngine.UI;

namespace MuseDashMirror.UICreate
{
    public static class TextGameObjectCreate
    {
        // 去掉最后，parent设置两个重载，名字和gameobject

        /// <summary>
        /// Create GameObject with specified canvas, with normal and white font and custom fontsize
        /// </summary>
        public static GameObject CreateTextGameObject(string canvasName, string gameObjectName, string text, TextAnchor alignment, bool isLocalPosition, Vector3 position, Vector2 sizeDelta, int fontSize)
        {
            var canvas = GameObject.Find(canvasName);
            var gameobject = new GameObject(gameObjectName);
            gameobject.transform.SetParent(canvas.transform);
            Text gameobject_text = gameobject.AddComponent<Text>();
            gameobject_text.text = text;
            gameobject_text.alignment = alignment;
            gameobject_text.font = Fonts.NormalFont;
            gameobject_text.fontSize = fontSize;
            gameobject_text.color = Color.white;
            if (isLocalPosition)
            {
                gameobject_text.transform.localPosition = position;
            }
            else
            {
                gameobject_text.transform.position = position;
            }
            RectTransform rectTransform = gameobject_text.GetComponent<RectTransform>();
            rectTransform.sizeDelta = sizeDelta;
            rectTransform.localScale = new Vector3(1, 1, 1);
            return gameobject;
        }

        /// <summary>
        /// Create GameObject with specified canvas, with white font and custom fontsize
        /// </summary>
        public static GameObject CreateTextGameObject(string canvasName, string gameObjectName, string text, TextAnchor alignment, bool isLocalPosition, Vector3 position, Vector2 sizeDelta, int fontSize, Font font)
        {
            var gameobject = CreateTextGameObject(canvasName, gameObjectName, text, alignment, isLocalPosition, position, sizeDelta, fontSize);
            Text gameobject_text = gameobject.GetComponent<Text>();
            gameobject_text.font = font;
            return gameobject;
        }

        /// <summary>
        /// Create GameObject with specified canvas, with custom font, fontsize and color
        /// </summary>
        public static GameObject CreateTextGameObject(string canvasName, string gameObjectName, string text, TextAnchor alignment, bool isLocalPosition, Vector3 position, Vector2 sizeDelta, int fontSize, Font font, Color color)
        {
            var gameobject = CreateTextGameObject(canvasName, gameObjectName, text, alignment, isLocalPosition, position, sizeDelta, fontSize, font);
            Text gameobject_text = gameobject.GetComponent<Text>();
            gameobject_text.color = color;
            return gameobject;
        }

        /// <summary>
        /// Create GameObject with specified canvas, with custom font, fontsize and color, custom local scale
        /// </summary>
        public static GameObject CreateTextGameObject(string canvasName, string gameObjectName, string text, TextAnchor alignment, bool isLocalPosition, Vector3 position, Vector2 sizeDelta, int fontSize, Font font, Color color, Vector3 localScale)
        {
            var gameobject = CreateTextGameObject(canvasName, gameObjectName, text, alignment, isLocalPosition, position, sizeDelta, fontSize, font, color);
            RectTransform rectTransform = gameobject.GetComponent<Text>().GetComponent<RectTransform>();
            rectTransform.localScale = localScale;
            return gameobject;
        }
    }
}