namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(PnlVictory), "OnVictory")]
internal static class PnlVictoryPatch
{
    private static void Postfix() => BattleComponent.OnVictoryEventInvoke();
}