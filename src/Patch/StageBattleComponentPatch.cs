using FormulaBase;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(StageBattleComponent), "GameStart")]
internal static class StageBattleComponentPatch
{
    private static void Postfix(StageBattleComponent __instance)
    {
        BattleComponent.GameStartEventInvoke();
        foreach (var musicData in __instance.GetMusicData())
        {
            BattleComponent.MusicDatas.Add(musicData);
        }
    }
}