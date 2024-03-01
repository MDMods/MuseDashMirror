namespace MuseDashMirror.Models;

/// <summary>
///     Text parameters for creating Ellipse Text GameObject
/// </summary>
public class EllipseTextParameters : TextParameters
{
    /// <summary>
    ///     Visible text range, default is the whole text
    /// </summary>
    public Range InVisibleTextRange { get; }

    /// <summary>
    ///     Create EllipseTextParameters with text content and invisible text range
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="inVisibleTextRange">Invisible Text Range</param>
    public EllipseTextParameters(string text, Range inVisibleTextRange) : base(text) => InVisibleTextRange = inVisibleTextRange;

    /// <summary>
    ///     Create EllipseTextParameters with text content, invisible text range and color
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="inVisibleTextRange">Invisible Text Range</param>
    /// <param name="color">Text Color</param>
    public EllipseTextParameters(string text, Range inVisibleTextRange, Color color) : base(text, color) => InVisibleTextRange = inVisibleTextRange;

    /// <summary>
    ///     Create EllipseTextParameters with text content, invisible text range and alignment
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="inVisibleTextRange">Invisible Text Range</param>
    /// <param name="alignment">Text Alignment</param>
    public EllipseTextParameters(string text, Range inVisibleTextRange, TextAnchor alignment)
        : base(text, alignment) => InVisibleTextRange = inVisibleTextRange;

    /// <summary>
    ///     Create EllipseTextParameters with text content, invisible text range, color and alignment
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="inVisibleTextRange">Invisible Text Range</param>
    /// <param name="color">Text Color</param>
    /// <param name="alignment">Text Alignment</param>
    public EllipseTextParameters(string text, Range inVisibleTextRange, Color color, TextAnchor alignment)
        : base(text, color, alignment) => InVisibleTextRange = inVisibleTextRange;

    /// <summary>
    ///     Create EllipseTextParameters with text content, invisible text range, font and font size
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="inVisibleTextRange">Invisible Text Range</param>
    /// <param name="font">Text Font</param>
    /// <param name="fontSize">Text Font Size</param>
    public EllipseTextParameters(string text, Range inVisibleTextRange, Font font, int fontSize)
        : base(text, font, fontSize) => InVisibleTextRange = inVisibleTextRange;

    /// <summary>
    ///     Create EllipseTextParameters with text content, invisible text range, font size and alignment
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="inVisibleTextRange">Invisible Text Range</param>
    /// <param name="fontSize">Text Font Size</param>
    /// <param name="alignment">Text Alignment</param>
    public EllipseTextParameters(string text, Range inVisibleTextRange, int fontSize, TextAnchor alignment)
        : base(text, fontSize, alignment) => InVisibleTextRange = inVisibleTextRange;

    /// <summary>
    ///     Create EllipseTextParameters with text content, invisible text range, font, font size and alignment
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="inVisibleTextRange">Invisible Text Range</param>
    /// <param name="font">Text Font</param>
    /// <param name="fontSize">Text Font Size</param>
    /// <param name="alignment">Text Alignment</param>
    public EllipseTextParameters(string text, Range inVisibleTextRange, Font font, int fontSize, TextAnchor alignment)
        : base(text, font, fontSize, alignment) => InVisibleTextRange = inVisibleTextRange;

    /// <summary>
    ///     Create EllipseTextParameters with text content, invisible text range, color, font and font size
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="inVisibleTextRange">Invisible Text Range</param>
    /// <param name="color">Text Color</param>
    /// <param name="font">Text Font</param>
    /// <param name="fontSize">Text Font Size</param>
    public EllipseTextParameters(string text, Range inVisibleTextRange, Color color, Font font, int fontSize)
        : base(text, color, font, fontSize) => InVisibleTextRange = inVisibleTextRange;

    /// <summary>
    ///     Create EllipseTextParameters with text content, invisible text range, color, font, font size and alignment
    /// </summary>
    /// <param name="text">Text Content</param>
    /// <param name="inVisibleTextRange">Invisible Text Range</param>
    /// <param name="color">Text Color</param>
    /// <param name="font">Text Font</param>
    /// <param name="fontSize">Text Font Size</param>
    /// <param name="alignment">Text Alignment</param>
    public EllipseTextParameters(string text, Range inVisibleTextRange, Color color, Font font, int fontSize, TextAnchor alignment)
        : base(text, color, font, fontSize, alignment) => InVisibleTextRange = inVisibleTextRange;

    /// <summary>
    ///     <inheritdoc cref="TextParameters.GetText" />
    /// </summary>
    /// <returns></returns>
    public override string GetText() => Text.GetVisibleTextWithEllipsisOrDefault(InVisibleTextRange);
}