namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(MusicInfo), nameof(MusicInfo.GetLocal))]
internal static class GetLocalPatch
{
    private static void Postfix(LocalALBUMInfo __result) => BattleComponent.ChartName = __result.name;
}