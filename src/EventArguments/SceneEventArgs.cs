namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event arguments for scene event
/// </summary>
/// <param name="buildIndex"></param>
/// <param name="sceneName"></param>
public class SceneEventArgs(int buildIndex, string sceneName) : EventArgs
{
    /// <summary>
    ///     Build index of the scene
    /// </summary>
    public int BuildIndex { get; set; } = buildIndex;

    /// <summary>
    ///     Name of the scene
    /// </summary>
    public string SceneName { get; set; } = sceneName;
}