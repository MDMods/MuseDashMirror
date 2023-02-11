using FormulaBase;
using GameLogic;
using System.Collections.Generic;

namespace MuseDashMirror.Patch
{
    [HarmonyPatch(typeof(StageBattleComponent), "GameStart")]
    internal static class StageBattleComponentPatch
    {
        internal static List<MusicData> musicDatas { get; set; } = new List<MusicData>();

        private static void Postfix(StageBattleComponent __instance)
        {
            BattleComponent.GameStartEventInvoke();
            foreach (var musicdata in __instance.GetMusicData())
            {
                musicDatas.Add(musicdata);
            }
        }
    }
}