using Assets.Scripts.Database;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(MusicInfo), "GetLocal")]
internal static class GetLocalPatch
{
    private static void Postfix(LocalALBUMInfo __result) => BattleComponent.ChartName = __result.name;
}