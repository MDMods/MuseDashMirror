namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(PnlStage), nameof(PnlStage.Awake))]
internal static class PnlStagePatch
{
    private static void Postfix() => PnlStageEventInvoke();
}