using Assets.Scripts.Database;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(MusicInfo), "GetLocal")]
internal static class GetLocalPatch
{
    internal static string ChartName { get; set; }

    private static void Postfix(LocalALBUMInfo __result) => ChartName = __result.name;
}