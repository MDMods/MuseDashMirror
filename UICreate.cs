using Assets.Scripts.PeroTools.Nice.Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace MuseDashMirror
{
    public static class UICreate
    {
        #region Fonts

        /// <summary>
        /// Snaps Taste font
        /// </summary>
        public static Font SnapsTasteFont { get; set; }

        /// <summary>
        /// Normal font
        /// </summary>
        public static Font NormalFont { get; set; }

        /// <summary>
        /// SourceHanSansCN-Heavy Font
        /// </summary>
        public static Font SourceHanSansCN_HeavyFont { get; set; }

        /// <summary>
        /// MiniSimpleSuperThickBlack Font
        /// </summary>
        public static Font MiniSimpleSuperThickBlackFont { get; set; }

        /// <summary>
        /// Load 4 default fonts
        /// </summary>
        public static void LoadFonts()
        {
            var snapstaste = Addressables.LoadAssetAsync<Font>("Snaps Taste");
            SnapsTasteFont = snapstaste.WaitForCompletion();
            var normal = Addressables.LoadAssetAsync<Font>("Normal");
            NormalFont = normal.WaitForCompletion();
            var arial = Addressables.LoadAssetAsync<Font>("SourceHanSansCN-Heavy");
            SourceHanSansCN_HeavyFont = arial.WaitForCompletion();
            var arialbold = Addressables.LoadAssetAsync<Font>("MiniSimpleSuperThickBlack");
            MiniSimpleSuperThickBlackFont = arialbold.WaitForCompletion();
        }

        /// <summary>
        /// Release memory after using 4 default fonts
        /// </summary>
        public static void UnloadFonts()
        {
            if (SnapsTasteFont != null)
            {
                Addressables.Release(SnapsTasteFont);
            }
            if (NormalFont != null)
            {
                Addressables.Release(NormalFont);
            }
            if (SourceHanSansCN_HeavyFont != null)
            {
                Addressables.Release(SourceHanSansCN_HeavyFont);
            }
            if (MiniSimpleSuperThickBlackFont != null)
            {
                Addressables.Release(MiniSimpleSuperThickBlackFont);
            }
        }

        #endregion Fonts

        #region Color

        /// <summary>
        /// Custom color blue
        /// </summary>
        public static Color Blue { get => new Color(0, 136 / 255f, 1, 1); }

        /// <summary>
        /// Custom color Silver
        /// </summary>
        public static Color Silver { get => new Color(192 / 255f, 192 / 255f, 192 / 255f, 1); }

        /// <summary>
        /// Default text color for toggle
        /// </summary>
        public static Color ToggleTextColor { get => new Color(1, 1, 1, 76 / 255f); }

        /// <summary>
        /// Default checkbox color for toggle
        /// </summary>
        public static Color ToggleCheckBoxColor { get => new Color(60 / 255f, 40 / 255f, 111 / 255f); }

        /// <summary>
        /// Default checkmark color for toggle
        /// </summary>
        public static Color ToggleCheckMarkColor { get => new Color(103 / 255f, 93 / 255f, 130 / 255f); }

        #endregion Color

        #region Create Canvas

        /// <summary>
        /// Create a screenspace overlay canvas
        /// </summary>
        /// <param name="canvasName"></param>
        public static void CreateCanvas(string canvasName)
        {
            var canvas = new GameObject();
            canvas.name = canvasName;
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        }

        /// <summary>
        /// Create a screenspace camera canvas with 1920*1080 reference resolution
        /// </summary>
        public static GameObject CreateCanvas(string canvasName, string cameraName)
        {
            var canvas = new GameObject();
            canvas.name = canvasName;
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            canvas.GetComponent<Canvas>().worldCamera = GameObject.Find(cameraName).GetComponent<Camera>();
            canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
            canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvas.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            return canvas;
        }

        /// <summary>
        /// Create a screenspace camera canvas with specified reference resolution
        /// </summary>
        public static void CreateCanvas(string canvasName, string cameraName, Vector2 referenceResolution)
        {
            var canvas = CreateCanvas(canvasName, cameraName);
            canvas.GetComponent<CanvasScaler>().referenceResolution = referenceResolution;
        }

        #endregion Create Canvas

        #region Create Text GameObject

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
            gameobject_text.font = NormalFont;
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

        #endregion Create Text GameObject

        #region Create PnlMenu Toggle

        /// <summary>
        /// Create Toggle at PnlMenu with fontsize 40
        /// </summary>
        public static unsafe GameObject CreatePnlMenuToggle(string name, Vector3 position, bool* isEnabled, string text)
        {
            var toggle = Object.Instantiate(GameObject.Find("Forward").transform.Find("PnlVolume").Find("LogoSetting").Find("Toggles").Find("TglOn").gameObject, GameObject.Find("PnlMenu").transform);
            toggle.name = name;

            var txt = toggle.transform.Find("Txt").GetComponent<Text>();
            var checkBox = toggle.transform.Find("Background").GetComponent<Image>();
            var checkMark = toggle.transform.Find("Background").GetChild(0).GetComponent<Image>();

            toggle.transform.position = position;
            toggle.GetComponent<OnToggle>().enabled = false;
            toggle.GetComponent<OnToggleOn>().enabled = false;
            toggle.GetComponent<OnActivate>().enabled = false;

            bool enabled = *isEnabled;
            var toggleComp = toggle.GetComponent<Toggle>();
            toggleComp.onValueChanged.AddListener((UnityEngine.Events.UnityAction<bool>)((val) => *isEnabled = val));
            toggleComp.group = null;
            toggleComp.SetIsOnWithoutNotify(enabled);

            txt.text = text;
            txt.fontSize = 40;
            txt.color = ToggleTextColor;
            var rect = txt.transform.Cast<RectTransform>();
            var vect = rect.offsetMax;
            rect.offsetMax = new Vector2(name.Length * 25, vect.y);

            checkBox.color = ToggleCheckBoxColor;
            checkMark.color = ToggleCheckMarkColor;

            return toggle;
        }

        /// <summary>
        /// Create Toggle at PnlMenu with custom local scale
        /// </summary>
        public static unsafe GameObject CreatePnlMenuToggle(string name, Vector3 position, bool* isEnabled, string text, Vector3 localScale)
        {
            var toggle = CreatePnlMenuToggle(name, position, isEnabled, text);
            toggle.transform.localScale = localScale;
            return toggle;
        }

        /// <summary>
        /// Create Toggle at PnlMenu with specified toggle group
        /// </summary>
        public static unsafe GameObject CreatePnlMenuToggle(string name, Vector3 position, bool* isEnabled, string text, ToggleGroup toggleGroup)
        {
            var toggle = CreatePnlMenuToggle(name, position, isEnabled, text);
            toggleGroup.RegisterToggle(toggle.GetComponent<Toggle>());
            return toggle;
        }

        /// <summary>
        /// Create Toggle at PnlMenu with custom fontsize and text color
        /// </summary>
        public static unsafe GameObject CreatePnlMenuToggle(string name, Vector3 position, bool* isEnabled, string text, int fontSize, Color textColor)
        {
            var toggle = CreatePnlMenuToggle(name, position, isEnabled, text);
            var txt = toggle.transform.Find("Txt").GetComponent<Text>();
            txt.fontSize = fontSize;
            txt.color = textColor;
            return toggle;
        }

        /// <summary>
        /// Create Toggle at PnlMenu with specified toggle group, custom fontsize and text color
        /// </summary>
        public static unsafe GameObject CreatePnlMenuToggle(string name, Vector3 position, bool* isEnabled, string text, int fontSize, Color textColor, ToggleGroup toggleGroup)
        {
            var toggle = CreatePnlMenuToggle(name, position, isEnabled, text, fontSize, textColor);
            toggleGroup.RegisterToggle(toggle.GetComponent<Toggle>());
            return toggle;
        }

        /// <summary>
        /// Create Toggle at PnlMenu with custom fontSize, textcolor, checkbox color and checkmark color
        /// </summary>
        public static unsafe GameObject CreatePnlMenuToggle(string name, Vector3 position, bool* isEnabled, string text, int fontSize, Color textColor, Color checkBoxColor, Color checkMarkColor)
        {
            var toggle = CreatePnlMenuToggle(name, position, isEnabled, text, fontSize, textColor);
            var checkBox = toggle.transform.Find("Background").GetComponent<Image>();
            checkBox.color = checkBoxColor;
            var checkMark = toggle.transform.Find("Background").GetChild(0).GetComponent<Image>();
            checkMark.color = checkMarkColor;
            return toggle;
        }

        #endregion Create PnlMenu Toggle
    }
}