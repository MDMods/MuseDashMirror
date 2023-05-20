using UnityEngine;

namespace MuseDashMirror.UICreate;

/// <summary>
/// Default colors
/// </summary>
public static class Colors
{
    /// <summary>
    /// Custom color blue
    /// </summary>
    public static Color Blue => new(0, 136 / 255f, 1, 1);

    /// <summary>
    /// Custom color Silver
    /// </summary>
    public static Color Silver => new(192 / 255f, 192 / 255f, 192 / 255f, 1);

    /// <summary>
    /// Default text color for toggle
    /// </summary>
    public static Color ToggleTextColor => new(1, 1, 1, 76 / 255f);

    /// <summary>
    /// Default checkbox color for toggle
    /// </summary>
    public static Color ToggleCheckBoxColor => new(60 / 255f, 40 / 255f, 111 / 255f);

    /// <summary>
    /// Default checkmark color for toggle
    /// </summary>
    public static Color ToggleCheckMarkColor => new(103 / 255f, 93 / 255f, 130 / 255f);
}