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
        var scaleFactor = rectTransform.gameObject.GetTotalScaleFactor();
        var canvasScalerFactor = rectTransform.gameObject.FindComponentInAncestors<CanvasScaler>().referenceResolution.x / Screen.width;
        var halfWidth = rectTransform.rect.width / 2;
        var position = transformParameters.Position;

        if (transformParameters.IsLocalPosition)
        {
            rectTransform.localPosition = new Vector3(position.x + halfWidth, position.y, position.z);
        }
        else
        {
            halfWidth *= scaleFactor.x;
            rectTransform.position = new Vector3(position.x * canvasScalerFactor + halfWidth, position.y * canvasScalerFactor, position.z);
        }
    }
}