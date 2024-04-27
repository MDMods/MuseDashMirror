using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Object = Il2CppSystem.Object;

namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(PnlVictory), nameof(PnlVictory.OnVictory), typeof(Object), typeof(Object), typeof(Il2CppReferenceArray<Object>))]
internal static class PnlVictoryPatch
{
    private static void Postfix(PnlVictory __instance) => PnlVictoryPatchInvoke(__instance);
}