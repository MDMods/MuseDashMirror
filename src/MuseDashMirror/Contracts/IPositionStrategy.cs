namespace MuseDashMirror.Contracts;

/// <summary>
///     Position Strategy Interface
/// </summary>
public interface IPositionStrategy
{
    /// <summary>
    ///     Set Position of the RectTransform
    /// </summary>
    /// <param name="rectTransform">RectTransform</param>
    /// <param name="transformParameters">TransformParameters</param>
    void SetPosition(RectTransform rectTransform, TransformParameters transformParameters);
}