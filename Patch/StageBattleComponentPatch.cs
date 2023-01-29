using FormulaBase;
using GameLogic;
using System.Collections.Generic;

namespace MuseDashMirror.Patch
{
    [HarmonyPatch(typeof(StageBattleComponent), "GameStart")]
    internal static class StageBattleComponentPatch
    {
        internal static List<MusicData> MusicDatas { get; set; } = new List<MusicData>();

        private static void Postfix(StageBattleComponent __instance)
        {
            foreach (var musicdata in __instance.GetMusicData())
            {
                MusicDatas.Add(musicdata);
            }
        }
    }
}