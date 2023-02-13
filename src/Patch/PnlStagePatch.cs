using Assets.Scripts.UI.Panels;
using MuseDashMirror.CommonPatches;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(PnlStage), "Awake")]
internal static class PnlStagePatch
{
    private static void Postfix(PnlStage __instance) => PatchEvents.PnlStageEventInvoke(__instance);
}