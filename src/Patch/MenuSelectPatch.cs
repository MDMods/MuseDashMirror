using MuseDashMirror.CommonPatches;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(MenuSelect), "OnToggleChanged")]
internal static class MenuSelectPatch
{
    private static void Postfix(int listIndex, int index, bool isOn)
    {
        PatchEvents.MenuSelectEventInvoke(listIndex, index, isOn);
    }
}