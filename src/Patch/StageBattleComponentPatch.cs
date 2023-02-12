using System.Collections.Generic;
using FormulaBase;
using GameLogic;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(StageBattleComponent), "GameStart")]
internal static class StageBattleComponentPatch
{
    internal static List<MusicData> MusicDatas { get; set; } = new();

    private static void Postfix(StageBattleComponent __instance)
    {
        BattleComponent.GameStartEventInvoke();
        foreach (var musicdata in __instance.GetMusicData())
        {
            MusicDatas.Add(musicdata);
        }
    }
}