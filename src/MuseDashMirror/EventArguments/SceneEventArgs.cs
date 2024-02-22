namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event Argument for Scene Events
/// </summary>
/// <param name="buildIndex"></param>
/// <param name="sceneName"></param>
public sealed class SceneEventArgs(int buildIndex, string sceneName) : EventArgs
{
    /// <summary>
    ///     Build index of the scene
    /// </summary>
    public int BuildIndex => buildIndex;

    /// <summary>
    ///     Name of the scene
    ///     <list type="bullet">
    ///         <item>"UISystem_PC" for Main Scene</item>
    ///         <item>"GameMain" for Game Scene</item>
    ///         <item>"Loading" for Loading Scene</item>
    ///         <item>"Welcome" for Welcome Scene</item>
    ///     </list>
    /// </summary>
    public string SceneName => sceneName;
}