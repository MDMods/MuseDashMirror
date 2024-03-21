using Il2CppFormulaBase;

namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(StageBattleComponent), nameof(StageBattleComponent.GameStart))]
internal static class StageBattleComponentPatch
{
    [UsedImplicitly]
    private static void Postfix(StageBattleComponent __instance)
    {
        GameStartPatchInvoke(__instance);
        Task.Run(() =>
        {
            foreach (var musicData in __instance.GetMusicData())
            {
                BattleComponent.MusicDataList.Add(musicData);
            }
        });
    }
}