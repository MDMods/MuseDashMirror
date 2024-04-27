using Il2CppAssets.Scripts.PeroTools.GeneralLocalization;
using Il2CppAssets.Scripts.PeroTools.Nice.Events;
using UnityEngine.Events;

namespace MuseDashMirror.Utils;

/// <summary>
///     Methods relates to create setting page
/// </summary>
[Logger]
public static partial class SettingUtils
{
    private static GameObject SettingButton { get; set; }
    private static GameObject SettingPage { get; set; }
    private static Transform SettingPageContentTransform { get; set; }

    internal static void CreateSettingButton()
    {
        var togglesTransform = GetGameObject("PnlOption").transform.GetChild(0);
        SettingButton = togglesTransform.GetChild(9).gameObject;

        // Comment this out because PPG HARD CODED THE NAME AND ANIMATION
        //SettingButton.name = "BtnModSetting";

        var img = SettingButton.GetChildGameObject(0);
        img.name = "ImgModSetting";
        var icon = ReadEmbeddedResource("Icon.png").ToSprite();
        icon.name = "IconModSetting";
        img.GetComponent<Image>().sprite = icon;

        var txt = SettingButton.GetChildGameObject(1);
        txt.name = "TxtModSetting";
        txt.GetComponent<Localization>().Destroy();
        txt.GetComponent<Text>().text = "Mod Setting";

        SettingButton.transform.SetSiblingIndex(14);
    }

    internal static void CreateSettingPage()
    {
        var forwardTransform = GetGameObject("UI").transform.GetChild(2);
        SettingPage = forwardTransform.GetChild(6).gameObject;
        //SettingPage.name = "PnlModSetting";

        var titleTextGameObject = SettingPage.GetChildGameObject(3, 0);
        titleTextGameObject.GetComponent<Localization>().DestroyImmediate();
        titleTextGameObject.GetComponent<Text>().text = "Mod Setting";

        SettingPageContentTransform = SettingPage.GetChildTransform(2, 0, 0);
        SettingPageContentTransform.DestroyAllChildren();

        SettingPage.transform.SetSiblingIndex(11);
        GameObjectCache["PnlVolume"] = forwardTransform.GetChildGameObject(5);
    }

    /// <summary>
    ///     Create a slider in the setting page
    /// </summary>
    /// <param name="name">Slider Name</param>
    /// <param name="min">Slider Min Value</param>
    /// <param name="max">Slider Max Value</param>
    /// <param name="callback">Callback for changing property value</param>
    /// <returns>Slider GameObject</returns>
    public static GameObject CreateSlider(string name, int min, int max, Action<int> callback)
    {
        var pnlVolume = GetGameObject("PnlVolume");
        var sliderContainer = pnlVolume.GetChildGameObject(3).FastInstantiate(SettingPageContentTransform);
        sliderContainer.name = name.Contains("Slider") ? name : $"{name}Slider";

        var sliderGameObject = sliderContainer.GetChildGameObject(0);
        sliderGameObject.GetComponent<OnSliderValueChanged>().DestroyImmediate();
        sliderGameObject.GetComponent<OnStart>().DestroyImmediate();
        sliderGameObject.name = name;

        var slider = sliderGameObject.GetComponent<Slider>();
        slider.minValue = min;
        slider.maxValue = max;

        // Slider Title
        var txt = sliderGameObject.GetChildGameObject(2);
        txt.GetComponent<Localization>().DestroyImmediate();
        txt.name = $"Txt{name}";
        txt.GetComponent<Text>().text = $"{name}:";

        // Slider Value
        var txtValue = txt.GetChildGameObject(0);
        txtValue.name = $"Txt{name}Value";

        slider.onValueChanged.AddListener((UnityAction<float>)((float value) => txtValue.GetComponent<Text>().text = $"{(int)value}"));
        slider.onValueChanged.AddListener((UnityAction<float>)((float value) => callback((int)value)));

        return sliderContainer;
    }
}