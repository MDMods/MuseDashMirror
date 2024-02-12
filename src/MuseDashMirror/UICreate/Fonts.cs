using UnityEngine.AddressableAssets;

namespace MuseDashMirror.UICreate;

/// <summary>
///     Default fonts
/// </summary>
public static class Fonts
{
    /// <summary>
    ///     Snaps Taste font
    /// </summary>
    public static Font SnapsTasteFont { get; private set; }

    /// <summary>
    ///     Normal font
    /// </summary>
    public static Font NormalFont { get; private set; }

    /// <summary>
    ///     SourceHanSansCN-Heavy Font
    /// </summary>
    public static Font SourceHanSansCnHeavyFont { get; private set; }

    /// <summary>
    ///     MiniSimpleSuperThickBlack Font
    /// </summary>
    public static Font MiniSimpleSuperThickBlackFont { get; private set; }

    /// <summary>
    ///     Load 4 default fonts
    /// </summary>
    internal static void LoadFonts()
    {
        SnapsTasteFont = LoadFont("Snaps Taste");
        NormalFont = LoadFont("Normal");
        SourceHanSansCnHeavyFont = LoadFont("SourceHanSansCN-Heavy");
        MiniSimpleSuperThickBlackFont = LoadFont("MiniSimpleSuperThickBlack");
    }

    /// <summary>
    ///     Release memory after using 4 default fonts
    /// </summary>
    internal static void UnloadFonts()
    {
        UnloadFont(SnapsTasteFont);
        UnloadFont(NormalFont);
        UnloadFont(SourceHanSansCnHeavyFont);
        UnloadFont(MiniSimpleSuperThickBlackFont);
    }

    private static Font LoadFont(string fontName) => Addressables.LoadAssetAsync<Font>(fontName).WaitForCompletion();

    private static void UnloadFont(Font font)
    {
        if (font != null)
        {
            Addressables.Release(font);
        }
    }
}