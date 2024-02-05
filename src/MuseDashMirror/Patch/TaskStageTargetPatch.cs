namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(TaskStageTarget), nameof(TaskStageTarget.AddScore))]
internal static class TaskStageTargetPatch
{
    private static void Postfix() => BattleComponent.AddScoreEventInvoke();
}