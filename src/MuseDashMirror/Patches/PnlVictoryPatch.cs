using Object = Il2CppSystem.Object;

namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(PnlVictory), nameof(PnlVictory.OnVictory), typeof(Object), typeof(Object), typeof(Object[]))]
internal static class PnlVictoryPatch
{
    [UsedImplicitly]
    private static void Postfix(PnlVictory __instance) => PnlVictoryPatchInvoke(__instance);
}