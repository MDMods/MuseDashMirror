using MuseDashMirror.Models.PositionStrategies;

namespace MuseDashMirror.Models;

/// <summary>
///     Transform parameters for creating GameObject
/// </summary>
public class TransformParameters
{
    /// <summary>
    ///     If true, the size of the RectTransform will be automatically adjusted to fit the text
    /// </summary>
    public bool IsAutoSize { get; } = true;

    /// <summary>
    ///     Is Local Position or Global Position
    /// </summary>
    public bool IsLocalPosition { get; set; }

    /// <summary>
    ///     Position of the GameObject
    /// </summary>
    public Vector3 Position { get; private set; }

    /// <summary>
    ///     Size of the RectTransform
    /// </summary>
    public Vector2 SizeDelta { get; private set; }

    /// <summary>
    ///     Scale of the GameObject
    /// </summary>
    public Vector3 LocalScale { get; set; } = new(1, 1, 1);

    /// <summary>
    ///     Position Strategy
    /// </summary>
    public IPositionStrategy PositionStrategy { get; set; } = new CenterPositionStrategy();

    /// <summary>
    ///     Create TransformParameters with Global Position
    /// </summary>
    /// <param name="position">GameObject Position</param>
    public TransformParameters(Vector3 position) => Position = position;

    /// <summary>
    ///     Create TransformParameters with Global Position and Position Strategy
    /// </summary>
    /// <param name="position">GameObject Position</param>
    /// <param name="positionStrategy">Position Strategy</param>
    public TransformParameters(Vector3 position, IPositionStrategy positionStrategy) : this(position) => PositionStrategy = positionStrategy;

    /// <summary>
    ///     Create TransformParameters with position, choose Local Position or Global Position
    /// </summary>
    /// <param name="position">GameObject Position</param>
    /// <param name="isLocalPosition">Local Position or not</param>
    public TransformParameters(Vector3 position, bool isLocalPosition)
    {
        Position = position;
        IsLocalPosition = isLocalPosition;
    }

    /// <summary>
    ///     Create TransformParameters with Global Position and SizeDelta
    /// </summary>
    /// <param name="position">GameObject Position</param>
    /// <param name="sizeDelta">GameObject RectTransform SizeDelta</param>
    public TransformParameters(Vector3 position, Vector2 sizeDelta)
    {
        Position = position;
        SizeDelta = sizeDelta;
        IsAutoSize = false;
    }

    /// <summary>
    ///     Create TransformParameters with Global Position, SizeDelta and Position Strategy
    /// </summary>
    /// <param name="position">GameObject Position</param>
    /// <param name="isLocalPosition">Local Position or not</param>
    /// <param name="positionStrategy"></param>
    public TransformParameters(Vector3 position, bool isLocalPosition, IPositionStrategy positionStrategy)
        : this(position, isLocalPosition) => PositionStrategy = positionStrategy;

    /// <summary>
    ///     Create TransformParameters with Global Position,choose LocalPosition or GlobalPosition, SizeDelta
    /// </summary>
    /// <param name="position">GameObject Position</param>
    /// <param name="isLocalPosition">Local Position or not</param>
    /// <param name="sizeDelta">GameObject RectTransform SizeDelta</param>
    public TransformParameters(Vector3 position, bool isLocalPosition, Vector2 sizeDelta)
        : this(position, sizeDelta) => IsLocalPosition = isLocalPosition;
}