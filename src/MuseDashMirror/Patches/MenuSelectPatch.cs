namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(MenuSelect), nameof(MenuSelect.OnToggleChanged))]
internal static class MenuSelectPatch
{
    private static void Postfix(int listIndex, int index, bool isOn) => MenuSelectInvoke(listIndex, index, isOn);
}