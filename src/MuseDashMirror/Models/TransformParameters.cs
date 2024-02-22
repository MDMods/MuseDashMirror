namespace MuseDashMirror.Models;

/// <summary>
///     Transform parameters for creating GameObject
/// </summary>
public class TransformParameters
{
    /// <summary>
    ///     If true, the size of the RectTransform will be automatically adjusted to fit the text
    /// </summary>
    public bool IsAutoSize { get; set; } = true;

    /// <summary>
    ///     Is LocalPosition or GlobalPosition
    /// </summary>
    public bool IsLocalPosition { get; set; } = false;

    /// <summary>
    ///     Position of the GameObject
    /// </summary>
    public Vector3 Position { get; set; }

    /// <summary>
    ///     Size of the RectTransform
    /// </summary>
    public Vector2 SizeDelta { get; set; }

    /// <summary>
    ///     Scale of the GameObject
    /// </summary>
    public Vector3 LocalScale { get; set; } = new(1, 1, 1);

    /// <summary>
    ///     Create TransformParameters with position
    /// </summary>
    /// <param name="position">GameObject Position</param>
    public TransformParameters(Vector3 position) => Position = position;
}