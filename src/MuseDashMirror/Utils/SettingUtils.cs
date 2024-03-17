using Il2CppAssets.Scripts.PeroTools.GeneralLocalization;

namespace MuseDashMirror.Utils;

internal static class SettingUtils
{
    private static GameObject SettingButton { get; set; }

    internal static Button CreateSettingButton()
    {
        var togglesTransform = GetGameObject("PnlOption").transform.GetChild(0);
        SettingButton = togglesTransform.GetChild(0).gameObject.FastInstantiate(togglesTransform);
        SettingButton.name = "BtnModSetting";

        var img = SettingButton.GetChildGameObject(0);
        img.name = "ImgModSetting";
        img.GetComponent<Image>().sprite = ReadEmbeddedResource("Icon.png").ToSprite();

        var txt = SettingButton.GetChildGameObject(1);
        txt.name = "TxtModSetting";
        txt.GetComponent<Localization>().Destroy();
        txt.GetComponent<Text>().text = "Mod Setting";

        return SettingButton.GetComponent<Button>();
    }
}