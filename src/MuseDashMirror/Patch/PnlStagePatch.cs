using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(PnlStage), nameof(PnlStage.Awake))]
internal static class PnlStagePatch
{
    private static void Postfix(PnlStage __instance) => PnlStageEventInvoke(__instance);
}