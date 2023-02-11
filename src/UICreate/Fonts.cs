using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MuseDashMirror.UICreate
{
    public static class Fonts
    {
        /// <summary>
        /// Snaps Taste font
        /// </summary>
        public static Font SnapsTasteFont { get; set; }

        /// <summary>
        /// Normal font
        /// </summary>
        public static Font NormalFont { get; set; }

        /// <summary>
        /// SourceHanSansCN-Heavy Font
        /// </summary>
        public static Font SourceHanSansCN_HeavyFont { get; set; }

        /// <summary>
        /// MiniSimpleSuperThickBlack Font
        /// </summary>
        public static Font MiniSimpleSuperThickBlackFont { get; set; }

        /// <summary>
        /// Load 4 default fonts
        /// </summary>
        public static void LoadFonts()
        {
            var snapstaste = Addressables.LoadAssetAsync<Font>("Snaps Taste");
            SnapsTasteFont = snapstaste.WaitForCompletion();
            var normal = Addressables.LoadAssetAsync<Font>("Normal");
            NormalFont = normal.WaitForCompletion();
            var arial = Addressables.LoadAssetAsync<Font>("SourceHanSansCN-Heavy");
            SourceHanSansCN_HeavyFont = arial.WaitForCompletion();
            var arialbold = Addressables.LoadAssetAsync<Font>("MiniSimpleSuperThickBlack");
            MiniSimpleSuperThickBlackFont = arialbold.WaitForCompletion();
        }

        /// <summary>
        /// Release memory after using 4 default fonts
        /// </summary>
        public static void UnloadFonts()
        {
            if (SnapsTasteFont != null)
            {
                Addressables.Release(SnapsTasteFont);
            }
            if (NormalFont != null)
            {
                Addressables.Release(NormalFont);
            }
            if (SourceHanSansCN_HeavyFont != null)
            {
                Addressables.Release(SourceHanSansCN_HeavyFont);
            }
            if (MiniSimpleSuperThickBlackFont != null)
            {
                Addressables.Release(MiniSimpleSuperThickBlackFont);
            }
        }
    }
}