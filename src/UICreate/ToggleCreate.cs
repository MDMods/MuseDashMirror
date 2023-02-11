using Assets.Scripts.PeroTools.Nice.Events;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuseDashMirror.UICreate
{
    public static class ToggleCreate
    {
        public static event Action OnToggleClick;

        private static void OnToggleClickInvoke(bool val) => OnToggleClick?.Invoke();

        // 去掉最后，parent设置两个重载，名字和gameobject

        #region PnlMenu Toggle

        /// <summary>
        /// Create Toggle at PnlMenu with fontsize 40
        /// </summary>
        public static unsafe GameObject CreatePnlMenuToggle(string name, Vector3 position, bool* isEnabled, string text)
        {
            var toggle = UnityEngine.Object.Instantiate(GameObject.Find("Forward").transform.Find("PnlVolume").Find("LogoSetting").Find("Toggles").Find("TglOn").gameObject, GameObject.Find("PnlMenu").transform);
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
            toggleComp.onValueChanged.AddListener((UnityEngine.Events.UnityAction<bool>)OnToggleClickInvoke);
            toggleComp.group = null;
            toggleComp.SetIsOnWithoutNotify(enabled);

            txt.text = text;
            txt.fontSize = 40;
            txt.color = Colors.ToggleTextColor;
            var rect = txt.transform.Cast<RectTransform>();
            var vect = rect.offsetMax;
            rect.offsetMax = new Vector2(name.Length * 25, vect.y);

            checkBox.color = Colors.ToggleCheckBoxColor;
            checkMark.color = Colors.ToggleCheckMarkColor;

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

        #endregion PnlMenu Toggle
    }
}