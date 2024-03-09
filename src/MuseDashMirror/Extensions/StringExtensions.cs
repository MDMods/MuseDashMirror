namespace MuseDashMirror.Extensions;

/// <summary>
///     <see cref="string" /> Extension Methods
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     Get visible text with ellipsis
    /// </summary>
    /// <param name="originalText">Original Text</param>
    /// <param name="inVisibleTextRange">The range of text to be replaced by ellipsis</param>
    /// <returns>Result String</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string GetVisibleTextWithEllipsis(this string originalText, Range inVisibleTextRange)
    {
        var startValue = inVisibleTextRange.Start.Value;
        var endValue = inVisibleTextRange.End.Value;
        if (originalText.Length < startValue + endValue)
        {
            throw new ArgumentOutOfRangeException(nameof(inVisibleTextRange), inVisibleTextRange,
                @"The range is out of the bounds of the original text");
        }

        var prefixText = originalText[..startValue];
        var suffixText = originalText[^endValue..];

        return $"{prefixText}...{suffixText}";
    }

    /// <summary>
    ///     Try to get visible text with ellipsis<br />
    ///     If the range is out of the bounds of the original text, the original text will be returned
    /// </summary>
    /// <param name="originalText">Original Text</param>
    /// <param name="inVisibleTextRange">The range of text to be replaced by ellipsis</param>
    /// <returns>Result String</returns>
    public static string GetVisibleTextWithEllipsisOrDefault(this string originalText, Range inVisibleTextRange)
    {
        var startValue = inVisibleTextRange.Start.Value;
        var endValue = inVisibleTextRange.End.Value;
        if (originalText.Length < startValue + endValue)
        {
            return originalText;
        }

        var prefixText = originalText[..startValue];
        var suffixText = originalText[^endValue..];

        return $"{prefixText}...{suffixText}";
    }
}