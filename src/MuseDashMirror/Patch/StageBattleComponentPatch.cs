namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(StageBattleComponent), nameof(StageBattleComponent.GameStart))]
internal static class StageBattleComponentPatch
{
    private static void Postfix(StageBattleComponent __instance)
    {
        BattleComponent.GameStartEventInvoke();
        Task.Run(() =>
        {
            foreach (var musicData in __instance.GetMusicData())
            {
                BattleComponent.MusicDataList.Add(musicData);
            }
        });
    }
}