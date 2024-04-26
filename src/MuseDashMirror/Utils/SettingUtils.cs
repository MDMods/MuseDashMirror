using Il2CppAssets.Scripts.PeroTools.GeneralLocalization;

namespace MuseDashMirror.Utils;

internal static class SettingUtils
{
    private static GameObject SettingButton { get; set; }
    private static GameObject SettingPage { get; set; }

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

        SettingPage.transform.SetSiblingIndex(11);
    }
}