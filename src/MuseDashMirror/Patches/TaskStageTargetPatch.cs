using Il2CppAssets.Scripts.GameCore.HostComponent;

namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(TaskStageTarget), nameof(TaskStageTarget.AddScore))]
internal static class TaskStageTargetPatch
{
    [UsedImplicitly]
    private static void Postfix(TaskStageTarget __instance, int value, int id, string noteType, bool isAir, float time = -1f) =>
        AddScorePatchInvoke(__instance, value, id, noteType, isAir, time);
}