using Assets.Scripts.Database;

namespace MuseDashMirror.Patch
{
    [HarmonyPatch(typeof(SpecialSongManager), "HideBmsCheck")]
    internal static class HideBmsCheckPatch
    {
        internal static int difficulty { get; set; }
        internal static string musicAuthor { get; set; }
        internal static string chartLevel { get; set; }
        internal static string charter { get; set; }

        private static void Postfix(MusicInfo selectedMusic, ref int selectedDifficulty)
        {
            difficulty = selectedDifficulty;
            musicAuthor = selectedMusic.author;
            chartLevel = selectedMusic.GetMusicLevelStringByDiff(selectedDifficulty);
            charter = selectedMusic.GetLevelDesignerStringByIndex(selectedDifficulty);
        }
    }
}