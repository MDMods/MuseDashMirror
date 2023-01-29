using Assets.Scripts.Database;
using static MuseDashMirror.BattleComponent;

namespace MuseDashMirror.Patch
{
    [HarmonyPatch(typeof(MusicInfo), "GetLocal")]
    internal static class GetLocalPatch
    {
        private static void Postfix(LocalALBUMInfo __result)
        {
            ChartName = __result.name;
        }
    }
}