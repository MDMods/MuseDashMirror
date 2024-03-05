namespace MuseDashMirror.Models.PositionStrategies;

/// <summary>
///     Position Strategy for setting the <b>Left Edge of the GameObject</b> to be the position
/// </summary>
public sealed class LeftEdgePositionStrategy : IPositionStrategy
{
    /// <summary>
    ///     <inheritdoc cref="IPositionStrategy.SetPosition" />
    /// </summary>
    /// <param name="rectTransform"></param>
    /// <param name="transformParameters"></param>
    public void SetPosition(RectTransform rectTransform, TransformParameters transformParameters)
    {
        var scaledFactor = rectTransform.gameObject.GetTotalScaledFactor();
        var halfWidth = rectTransform.rect.width / 2;
        if (transformParameters.IsLocalPosition)
        {
            rectTransform.localPosition = new Vector3(transformParameters.Position.x + halfWidth,
                transformParameters.Position.y,
                transformParameters.Position.z);
        }
        else
        {
            halfWidth *= scaledFactor.x;
            rectTransform.position = new Vector3(transformParameters.Position.x + halfWidth,
                transformParameters.Position.y,
                transformParameters.Position.z);
        }
    }
}