using Il2CppAssets.Scripts.Database;

namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(MusicInfo), nameof(MusicInfo.GetLocal))]
internal static class GetLocalPatch
{
    [UsedImplicitly]
    private static void Postfix(LocalALBUMInfo __result) => BattleComponent.ChartName = __result.name;
}