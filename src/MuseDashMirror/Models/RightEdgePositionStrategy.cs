namespace MuseDashMirror.Models;

/// <summary>
///     Position Strategy for setting the <b>Right Edge of the GameObject</b> to be the position
/// </summary>
public sealed class RightEdgePositionStrategy : IPositionStrategy
{
    /// <summary>
    ///     <inheritdoc cref="IPositionStrategy.SetPosition" />
    /// </summary>
    /// <param name="rectTransform"></param>
    /// <param name="transformParameters"></param>
    public void SetPosition(RectTransform rectTransform, TransformParameters transformParameters)
    {
        var canvas = rectTransform.gameObject.FindComponentInAncestors<Canvas>();
        var halfWidth = rectTransform.rect.width / 2;
        if (transformParameters.IsLocalPosition)
        {
            rectTransform.localPosition = new Vector3(transformParameters.Position.x - halfWidth,
                transformParameters.Position.y,
                transformParameters.Position.z);
        }
        else
        {
            halfWidth *= canvas.gameObject.transform.localScale.x;
            rectTransform.position = new Vector3(transformParameters.Position.x - halfWidth,
                transformParameters.Position.y,
                transformParameters.Position.z);
        }
    }
}