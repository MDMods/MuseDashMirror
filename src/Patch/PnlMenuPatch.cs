using Assets.Scripts.UI.Panels;
using MuseDashMirror.CommonPatches;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(PnlMenu), "Awake")]
internal static class PnlMenuPatch
{
    private static void Postfix(PnlMenu __instance)
    {
        PatchEvents.PnlMenuEventInvoke(__instance);
    }
}