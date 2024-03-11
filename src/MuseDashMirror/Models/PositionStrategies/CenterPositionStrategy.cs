namespace MuseDashMirror.Models.PositionStrategies;

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
        var canvasScalerFactor = rectTransform.gameObject.FindComponentInAncestors<CanvasScaler>().referenceResolution.x / Screen.width;
        var position = transformParameters.Position;
        if (transformParameters.IsLocalPosition)
        {
            rectTransform.localPosition = position;
        }
        else
        {
            rectTransform.position = new Vector3(position.x * canvasScalerFactor, position.y * canvasScalerFactor, position.z);
        }
    }
}