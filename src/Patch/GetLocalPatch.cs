using Assets.Scripts.Database;

namespace MuseDashMirror.Patch
{
    [HarmonyPatch(typeof(MusicInfo), "GetLocal")]
    internal static class GetLocalPatch
    {
        internal static string chartName { get; set; }

        private static void Postfix(LocalALBUMInfo __result)
        {
            chartName = __result.name;
        }
    }
}