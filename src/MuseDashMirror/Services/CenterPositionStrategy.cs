namespace MuseDashMirror.Services;

/// <summary>
///     Position Strategy for setting the <b>Center of the GameObject</b> to be the position
/// </summary>
public sealed class CenterPositionStrategy : IPositionStrategy
{
    /// <summary>
    ///     <inheritdoc cref="IPositionStrategy.SetPosition" />
    /// </summary>
    /// <param name="rectTransform"></param>
    /// <param name="transformParameters"></param>
    public void SetPosition(RectTransform rectTransform, TransformParameters transformParameters)
    {
        if (transformParameters.IsLocalPosition)
        {
            rectTransform.localPosition = transformParameters.Position;
        }
        else
        {
            rectTransform.position = transformParameters.Position;
        }
    }
}