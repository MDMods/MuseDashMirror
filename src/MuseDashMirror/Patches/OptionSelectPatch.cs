namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(OptionSelect))]
internal static class OptionSelectPatch
{
    [HarmonyPatch(nameof(OptionSelect.OnAwake))]
    [HarmonyPostfix]
    private static void AwakePostfix(OptionSelect __instance)
    {
        CreateSettingButton();
        CreateSettingPage();
        __instance.SetButtonVisual(10, true);

        //var slider = CreateSlider("Test", 0, 80);
    }
}