using Assets.Scripts.Database;

namespace MuseDashMirror.Patch
{
    [HarmonyPatch(typeof(SpecialSongManager), "HideBmsCheck")]
    internal static class HideBmsCheckPatch
    {
        internal static int Difficulty { get; set; }
        internal static string MusicAuthor { get; set; }
        internal static string ChartLevel { get; set; }
        internal static string Charter { get; set; }

        private static void Postfix(MusicInfo selectedMusic, ref int selectedDifficulty)
        {
            Difficulty = selectedDifficulty;
            MusicAuthor = selectedMusic.author;
            ChartLevel = selectedMusic.GetMusicLevelStringByDiff(selectedDifficulty);
            Charter = selectedMusic.GetLevelDesignerStringByIndex(selectedDifficulty);
        }
    }
}