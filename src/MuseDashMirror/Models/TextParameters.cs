using MuseDashMirror.UIComponents;

namespace MuseDashMirror.Models;

/// <summary>
///     Text parameters for creating Text GameObject
/// </summary>
public class TextParameters
{
    /// <summary>
    ///     Text content
    /// </summary>
    public string Text { get; }

    /// <summary>
    ///     Text color, default is <see cref="Color.white" />
    /// </summary>
    public Color Color { get; set; } = Color.white;

    /// <summary>
    ///     Text font, default is <see cref="Fonts.NormalFont" />
    /// </summary>
    public Font Font { get; set; } = NormalFont;

    /// <summary>
    ///     Font size, default is 20
    /// </summary>
    public int FontSize { get; set; } = 20;

    /// <summary>
    ///     Alignment of the text, default is <see cref="TextAnchor.MiddleCenter" />
    /// </summary>
    public TextAnchor Alignment { get; set; } = TextAnchor.MiddleCenter;

    /// <summary>
    ///     Create TextParameters with text content
    /// </summary>
    /// <param name="text">Text Content</param>
    public TextParameters(string text) => Text = text;

    /// <summary>
    ///     Create TextParameters with text content and color
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="color">Text Color</param>
    public TextParameters(string text, Color color) : this(text) => Color = color;

    /// <summary>
    ///     Create TextParameters with text content and alignment
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="alignment">Text Alignment</param>
    public TextParameters(string text, TextAnchor alignment) : this(text) => Alignment = alignment;

    /// <summary>
    ///     Create TextParameters with text content, color and alignment
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="color">Text Color</param>
    /// <param name="alignment">Text Alignment</param>
    public TextParameters(string text, Color color, TextAnchor alignment) : this(text, color) => Alignment = alignment;

    /// <summary>
    ///     Create TextParameters with text content, font and font size
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="font">Text Font</param>
    /// <param name="fontSize">Text Font Size</param>
    public TextParameters(string text, Font font, int fontSize) : this(text)
    {
        Font = font;
        FontSize = fontSize;
    }

    /// <summary>
    ///     Create TextParameters with text content, font size and alignment
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="fontSize">Text Font Size</param>
    /// <param name="alignment">Text Alignment</param>
    public TextParameters(string text, int fontSize, TextAnchor alignment) : this(text)
    {
        FontSize = fontSize;
        Alignment = alignment;
    }

    /// <summary>
    ///     Create TextParameters with text content, font, font size and alignment
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="font">Text Font</param>
    /// <param name="fontSize">Text Font Size</param>
    /// <param name="alignment">Text Alignment</param>
    public TextParameters(string text, Font font, int fontSize, TextAnchor alignment) : this(text, font, fontSize) => Alignment = alignment;

    /// <summary>
    ///     Create TextParameters with text content, color, font and font size
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="color">Text Color</param>
    /// <param name="font">Text Font</param>
    /// <param name="fontSize">Text Font Size</param>
    public TextParameters(string text, Color color, Font font, int fontSize) : this(text, font, fontSize) => Color = color;

    /// <summary>
    ///     Create TextParameters with text content, color, font, font size and alignment
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="color">Text Color</param>
    /// <param name="font">Text Font</param>
    /// <param name="fontSize">Text Font Size</param>
    /// <param name="alignment">Text Alignment</param>
    public TextParameters(string text, Color color, Font font, int fontSize, TextAnchor alignment)
        : this(text, color, font, fontSize) => Alignment = alignment;

    /// <summary>
    ///     Get the text content
    /// </summary>
    /// <returns></returns>
    public virtual string GetText() => Text;
}