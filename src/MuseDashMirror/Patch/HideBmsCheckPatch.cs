using Il2CppAssets.Scripts.Database;
using static MuseDashMirror.BattleComponent;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(SpecialSongManager), nameof(SpecialSongManager.HideBmsCheck))]
internal static class HideBmsCheckPatch
{
    private static void Postfix(MusicInfo selectedMusic, ref int selectedDifficulty)
    {
        Difficulty = selectedDifficulty;
        MusicAuthor = selectedMusic.author;
        ChartLevel = selectedMusic.GetMusicLevelStringByDiff(selectedDifficulty);
        Charter = selectedMusic.GetLevelDesignerStringByIndex(selectedDifficulty);
    }
}