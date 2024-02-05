using static MuseDashMirror.BattleComponent;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(GameMissPlay), nameof(GameMissPlay.MissCube))]
internal static class MissPatch
{
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
                        GhostMissNum++;
                        TotalMissNum++;
                        break;

                    // air collectable note miss
                    case 6 or 7:
                        CollectableNoteMissNum++;
                        TotalMissNum++;
                        break;

                    // normal miss
                    default:
                    {
                        if (musicDataByIdx.noteData.type != 2 && !musicDataByIdx.isDouble)
                        {
                            NormalMissNum++;
                            TotalMissNum++;
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
                        GhostMissNum++;
                        TotalMissNum++;
                        break;

                    // ground collectable note miss
                    case 6 or 7:
                        CollectableNoteMissNum++;
                        TotalMissNum++;
                        break;

                    // normal miss
                    default:
                        NormalMissNum++;
                        TotalMissNum++;
                        break;
                }

                break;
        }

        MissCubeEventInvoke();
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
                NormalMissNum++;
                TotalMissNum++;
            }

            MissCubeEventInvoke();
        }
    }
}