namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(OptionSelect))]
internal static class OptionSelectPatch
{
    [HarmonyPatch(nameof(OptionSelect.OnAwake))]
    [HarmonyPostfix]
    private static void AwakePostfix(OptionSelect __instance)
    {
        __instance.m_SelectedButtonList[16] = CreateSettingButton();
    }
}