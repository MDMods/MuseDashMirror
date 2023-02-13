using Assets.Scripts.GameCore.HostComponent;
using Assets.Scripts.PeroTools.Commons;
using FormulaBase;
using GameLogic;
using static MuseDashMirror.BattleComponent;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(GameMissPlay), "MissCube")]
internal static class GameMissPlayPatch
{
    internal static int GhostMiss { get; set; }
    internal static int CollectableNoteMiss { get; set; }
    internal static int NormalMiss { get; set; }
    internal static int TotalMiss { get; set; }

    private static void Postfix(int idx, decimal currentTick)
    {
        int result = Singleton<BattleEnemyManager>.instance.GetPlayResult(idx);
        var musicDataByIdx = Singleton<StageBattleComponent>.instance.GetMusicDataByIdx(idx);
        switch (result)
        {
            case 0:
                switch (musicDataByIdx.noteData.type)
                {
                    // air ghost miss
                    case 4:
                        GhostMiss++;
                        TotalMiss++;
                        break;

                    // air collectable note miss
                    case 6 or 7:
                        CollectableNoteMiss++;
                        TotalMiss++;
                        break;

                    // normal miss
                    default:
                    {
                        if (musicDataByIdx.noteData.type != 2 && !musicDataByIdx.isDouble)
                        {
                            NormalMiss++;
                            TotalMiss++;
                        }

                        break;
                    }
                }

                break;

            case 1:
                switch (musicDataByIdx.noteData.type)
                {
                    // ground ghost miss
                    case 4:
                        GhostMiss++;
                        TotalMiss++;
                        break;

                    // ground collectable note miss
                    case 6 or 7:
                        CollectableNoteMiss++;
                        TotalMiss++;
                        break;

                    // normal miss
                    default:
                        NormalMiss++;
                        TotalMiss++;
                        break;
                }

                break;
        }

        MissCubeEventInvoke();
    }

    internal static void Reset()
    {
        GhostMiss = 0;
        CollectableNoteMiss = 0;
        NormalMiss = 0;
        TotalMiss = 0;
    }

    // Hold miss
    [HarmonyPatch(typeof(BattleEnemyManager), "SetPlayResult")]
    internal static class SetPlayResultPatch
    {
        private static void Postfix(int idx, byte result, bool isMulStart = false, bool isMulEnd = false, bool isLeft = false)
        {
            var musicDataByIdx = Singleton<StageBattleComponent>.instance.GetMusicDataByIdx(idx);
            if (result == 1 && musicDataByIdx.noteData.type == 3)
            {
                NormalMiss++;
                TotalMiss++;
            }
        }
    }
}