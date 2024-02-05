/*using Il2CppAssets.Scripts.PeroTools.Nice.Events;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace MuseDashMirror.UICreate;

/// <summary>
/// Methods for creating toggle
/// </summary>
public static class ToggleCreate
{

    #region General Toggle Create

    /// <summary>
    /// Create general toggle with specific parent using name
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, string parentName)
    {
        var toggle = Object.Instantiate(GameObject.Find("Forward").transform.Find("PnlVolume").Find("LogoSetting").Find("Toggles").Find("TglOn").gameObject, GameObject.Find(parentName).transform);
        toggle.name = name;

        var txt = toggle.transform.Find("Txt").GetComponent<Text>();
        var checkBox = toggle.transform.Find("Background").GetComponent<Image>();
        var checkMark = toggle.transform.Find("Background").GetChild(0).GetComponent<Image>();

        toggle.transform.position = position;
        toggle.GetComponent<OnToggle>().enabled = false;
        toggle.GetComponent<OnToggleOn>().enabled = false;
        toggle.GetComponent<OnActivate>().enabled = false;

        var enabled = *isEnabled;
        var toggleComp = toggle.GetComponent<Toggle>();
        toggleComp.onValueChanged.AddListener((Action<bool>)((val) => *isEnabled = val));
        toggleComp.group = null;
        toggleComp.SetIsOnWithoutNotify(enabled);

        txt.text = text;
        txt.fontSize = 40;
        txt.color = Colors.ToggleTextColor;
        var rect = txt.transform.Cast<RectTransform>();
        var vect = rect.offsetMax;
        rect.offsetMax = new Vector2(text.Length * 25, vect.y);

        checkBox.color = Colors.ToggleCheckBoxColor;
        checkMark.color = Colors.ToggleCheckMarkColor;

        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent using name, with specified toggle group
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, string parentName, ToggleGroup toggleGroup)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parentName);
        toggle.GetComponent<Toggle>().group = toggleGroup;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent using name, with custom font size and text color
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, string parentName, int fontSize, Color textColor)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parentName);
        var txt = toggle.transform.Find("Txt").GetComponent<Text>();
        txt.fontSize = fontSize;
        txt.color = textColor;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent using name, with specified toggle group, custom font size and text color
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, string parentName, int fontSize, Color textColor, ToggleGroup toggleGroup)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parentName, fontSize, textColor);
        toggle.GetComponent<Toggle>().group = toggleGroup;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent using name, with custom fontSize, text color, checkbox color and checkmark color
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, string parentName, int fontSize, Color textColor, Color checkBoxColor, Color checkMarkColor)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parentName, fontSize, textColor);
        var checkBox = toggle.transform.Find("Background").GetComponent<Image>();
        checkBox.color = checkBoxColor;
        var checkMark = toggle.transform.Find("Background").GetChild(0).GetComponent<Image>();
        checkMark.color = checkMarkColor;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent using name, with custom fontSize, text color, checkbox color, checkmark color and toggle group
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, string parentName, int fontSize, Color textColor, Color checkBoxColor, Color checkMarkColor, ToggleGroup toggleGroup)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parentName, fontSize, textColor, checkBoxColor, checkMarkColor);
        toggle.GetComponent<Toggle>().group = toggleGroup;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, GameObject parent)
    {
        var toggle = Object.Instantiate(GameObject.Find("Forward").transform.Find("PnlVolume").Find("LogoSetting").Find("Toggles").Find("TglOn").gameObject, parent.transform);
        toggle.name = name;

        var txt = toggle.transform.Find("Txt").GetComponent<Text>();
        var checkBox = toggle.transform.Find("Background").GetComponent<Image>();
        var checkMark = toggle.transform.Find("Background").GetChild(0).GetComponent<Image>();

        toggle.transform.position = position;
        toggle.GetComponent<OnToggle>().enabled = false;
        toggle.GetComponent<OnToggleOn>().enabled = false;
        toggle.GetComponent<OnActivate>().enabled = false;

        var enabled = *isEnabled;
        var toggleComp = toggle.GetComponent<Toggle>();
        toggleComp.onValueChanged.AddListener((Action<bool>)((val) => *isEnabled = val));
        toggleComp.group = null;
        toggleComp.SetIsOnWithoutNotify(enabled);

        txt.text = text;
        txt.fontSize = 40;
        txt.color = Colors.ToggleTextColor;
        var rect = txt.transform.Cast<RectTransform>();
        var vect = rect.offsetMax;
        rect.offsetMax = new Vector2(text.Length * 25, vect.y);

        checkBox.color = Colors.ToggleCheckBoxColor;
        checkMark.color = Colors.ToggleCheckMarkColor;

        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent, with specified toggle group
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, GameObject parent, ToggleGroup toggleGroup)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parent);
        toggle.GetComponent<Toggle>().group = toggleGroup;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent, with custom font size and text color
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, GameObject parent, int fontSize, Color textColor)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parent);
        var txt = toggle.transform.Find("Txt").GetComponent<Text>();
        txt.fontSize = fontSize;
        txt.color = textColor;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent, with specified toggle group, custom font size and text color
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, GameObject parent, int fontSize, Color textColor, ToggleGroup toggleGroup)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parent, fontSize, textColor);
        toggle.GetComponent<Toggle>().group = toggleGroup;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent, with custom fontSize, text color, checkbox color and checkmark color
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, GameObject parent, int fontSize, Color textColor, Color checkBoxColor, Color checkMarkColor)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parent, fontSize, textColor);
        var checkBox = toggle.transform.Find("Background").GetComponent<Image>();
        checkBox.color = checkBoxColor;
        var checkMark = toggle.transform.Find("Background").GetChild(0).GetComponent<Image>();
        checkMark.color = checkMarkColor;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent, with custom fontSize, text color, checkbox color, checkmark color and toggle group
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, GameObject parent, int fontSize, Color textColor, Color checkBoxColor, Color checkMarkColor, ToggleGroup toggleGroup)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parent, fontSize, textColor, checkBoxColor, checkMarkColor);
        toggle.GetComponent<Toggle>().group = toggleGroup;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent transform
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, Transform parent)
    {
        var toggle = Object.Instantiate(GameObject.Find("Forward").transform.Find("PnlVolume").Find("LogoSetting").Find("Toggles").Find("TglOn").gameObject, parent);
        toggle.name = name;

        var txt = toggle.transform.Find("Txt").GetComponent<Text>();
        var checkBox = toggle.transform.Find("Background").GetComponent<Image>();
        var checkMark = toggle.transform.Find("Background").GetChild(0).GetComponent<Image>();

        toggle.transform.position = position;
        toggle.GetComponent<OnToggle>().enabled = false;
        toggle.GetComponent<OnToggleOn>().enabled = false;
        toggle.GetComponent<OnActivate>().enabled = false;

        var enabled = *isEnabled;
        var toggleComp = toggle.GetComponent<Toggle>();
        toggleComp.onValueChanged.AddListener((Action<bool>)((val) => *isEnabled = val));
        toggleComp.group = null;
        toggleComp.SetIsOnWithoutNotify(enabled);

        txt.text = text;
        txt.fontSize = 40;
        txt.color = Colors.ToggleTextColor;
        var rect = txt.transform.Cast<RectTransform>();
        var vect = rect.offsetMax;
        rect.offsetMax = new Vector2(text.Length * 25, vect.y);

        checkBox.color = Colors.ToggleCheckBoxColor;
        checkMark.color = Colors.ToggleCheckMarkColor;

        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent transform, with specified toggle group
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, Transform parent, ToggleGroup toggleGroup)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parent);
        toggle.GetComponent<Toggle>().group = toggleGroup;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent transform, with custom font size and text color
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, Transform parent, int fontSize, Color textColor)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parent);
        var txt = toggle.transform.Find("Txt").GetComponent<Text>();
        txt.fontSize = fontSize;
        txt.color = textColor;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent transform, with specified toggle group, custom font size and text color
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, Transform parent, int fontSize, Color textColor, ToggleGroup toggleGroup)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parent, fontSize, textColor);
        toggle.GetComponent<Toggle>().group = toggleGroup;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent transform, with custom fontSize, text color, checkbox color and checkmark color
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, Transform parent, int fontSize, Color textColor, Color checkBoxColor, Color checkMarkColor)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parent, fontSize, textColor);
        var checkBox = toggle.transform.Find("Background").GetComponent<Image>();
        checkBox.color = checkBoxColor;
        var checkMark = toggle.transform.Find("Background").GetChild(0).GetComponent<Image>();
        checkMark.color = checkMarkColor;
        return toggle;
    }

    /// <summary>
    /// Create general toggle with specific parent transform, with custom fontSize, text color, checkbox color, checkmark color and toggle group
    /// </summary>
    public static unsafe GameObject CreateToggle(string name, Vector3 position, bool* isEnabled, string text, Transform parent, int fontSize, Color textColor, Color checkBoxColor, Color checkMarkColor, ToggleGroup toggleGroup)
    {
        var toggle = CreateToggle(name, position, isEnabled, text, parent, fontSize, textColor, checkBoxColor, checkMarkColor);
        toggle.GetComponent<Toggle>().group = toggleGroup;
        return toggle;
    }

    #endregion General Toggle Create

    #region PnlMenu Toggle Create

    private static readonly List<string> Texts = new();
    private static Vector3[] Positions = new[] { new Vector3(-6.8f, -2.55f, 100f), new Vector3(-6.8f, -3.35f, 100f), new Vector3(-6.8f, -4.15f, 100f), new Vector3(-6.8f, -4.95f, 100f) };

    internal static void Reset()
    {
        Texts.Clear();
        Positions = new[] { new Vector3(-6.8f, -2.55f, 100f), new Vector3(-6.8f, -3.35f, 100f), new Vector3(-6.8f, -4.15f, 100f), new Vector3(-6.8f, -4.95f, 100f) };
    }

    private static void SetPosition(string text)
    {
        Texts.Add(text);
        if (Texts.Count == 4)
        {
            var longest = string.Empty;
            foreach (var txt in Texts)
            {
                if (longest.Length < txt.Length)
                {
                    longest = txt;
                }
            }

            for (var i = 0; i < Positions.Length; i++)
            {
                Positions[i].x += longest.Length * 0.3f;
            }

            Texts.Clear();
        }
    }

    /// <summary>
    /// Create Toggle at PnlMenu with font size 40
    /// </summary>
    public static unsafe GameObject CreatePnlMenuToggle(string name, bool* isEnabled, string text)
    {
        var toggle = CreateToggle(name, Positions[Texts.Count], isEnabled, text, "PnlMenu");
        SetPosition(text);
        return toggle;
    }

    /// <summary>
    /// Create Toggle at PnlMenu with toggle group
    /// </summary>
    public static unsafe GameObject CreatePnlMenuToggle(string name, bool* isEnabled, string text, string parentName, ToggleGroup toggleGroup)
    {
        var toggle = CreatePnlMenuToggle(name, isEnabled, text);
        toggle.transform.SetParent(GameObject.Find(parentName).transform);
        toggle.GetComponent<Toggle>().group = toggleGroup;
        return toggle;
    }

    /// <summary>
    /// Create Toggle at PnlMenu with toggle group
    /// </summary>
    public static unsafe GameObject CreatePnlMenuToggle(string name, bool* isEnabled, string text, GameObject parent, ToggleGroup toggleGroup)
    {
        var toggle = CreatePnlMenuToggle(name, isEnabled, text);
        toggle.transform.SetParent(parent.transform);
        toggle.GetComponent<Toggle>().group = toggleGroup;
        return toggle;
    }

    /// <summary>
    /// Create Toggle at PnlMenu with custom font size and text color
    /// </summary>
    public static unsafe GameObject CreatePnlMenuToggle(string name, bool* isEnabled, string text, int fontSize, Color textColor)
    {
        var toggle = CreatePnlMenuToggle(name, isEnabled, text);
        var txt = toggle.transform.Find("Txt").GetComponent<Text>();
        txt.fontSize = fontSize;
        txt.color = textColor;
        return toggle;
    }

    /// <summary>
    /// Create Toggle at PnlMenu with custom fontSize, text color, checkbox color and checkmark color
    /// </summary>
    public static unsafe GameObject CreatePnlMenuToggle(string name, bool* isEnabled, string text, int fontSize, Color textColor, Color checkBoxColor, Color checkMarkColor)
    {
        var toggle = CreatePnlMenuToggle(name, isEnabled, text, fontSize, textColor);
        var checkBox = toggle.transform.Find("Background").GetComponent<Image>();
        checkBox.color = checkBoxColor;
        var checkMark = toggle.transform.Find("Background").GetChild(0).GetComponent<Image>();
        checkMark.color = checkMarkColor;
        return toggle;
    }

    #endregion PnlMenu Toggle Create
}*/

