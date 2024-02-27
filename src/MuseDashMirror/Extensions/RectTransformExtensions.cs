using UnityEngine.UI;

namespace MuseDashMirror.Extensions;

/// <summary>
///     RectTransform Extension Methods
/// </summary>
public static class RectTransformExtensions
{
    /// <summary>
    ///     Update the layout information of the RectTransform
    /// </summary>
    /// <param name="rectTransform">RectTransform</param>
    public static void UpdateTransformLayoutInfo(this RectTransform rectTransform) => LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
}