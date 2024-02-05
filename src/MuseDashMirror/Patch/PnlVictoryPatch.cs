using Object = Il2CppSystem.Object;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(PnlVictory), nameof(PnlVictory.OnVictory), typeof(Object), typeof(Object), typeof(Object[]))]
internal static class PnlVictoryPatch
{
    private static void Postfix() => BattleComponent.OnVictoryEventInvoke();
}