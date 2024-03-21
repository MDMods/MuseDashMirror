using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(PnlStage), nameof(PnlStage.Awake))]
internal static class PnlStagePatch
{
    [UsedImplicitly]
    private static void Postfix(PnlStage __instance) => PnlStagePatchInvoke(__instance);
}