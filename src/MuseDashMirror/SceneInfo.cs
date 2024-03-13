namespace MuseDashMirror;

/// <summary>
///     Scene load/unload events
/// </summary>
public static class SceneInfo
{
    #region GeneralScene

    /// <summary>
    ///     An event to invoke methods when entering a scene
    /// </summary>
    public static event EventHandler<SceneEventArgs> OnEnterScene;

    /// <summary>
    ///     An event to invoke methods when exiting a scene
    /// </summary>
    public static event EventHandler<SceneEventArgs> OnExitScene;

    internal static void OnEnterSceneInvoke(int buildIndex, string sceneName) =>
        OnEnterScene?.Invoke(null, new SceneEventArgs(buildIndex, sceneName));

    internal static void OnExitSceneInvoke(int buildIndex, string sceneName) =>
        OnExitScene?.Invoke(null, new SceneEventArgs(buildIndex, sceneName));

    #endregion

    #region MainScene

    /// <summary>
    ///     Is in the Main Scene
    /// </summary>
    public static bool IsMainScene { get; internal set; }

    /// <summary>
    ///     An event to invoke methods when entering main scene
    /// </summary>
    public static event EventHandler<SceneEventArgs> OnEnterMainScene;

    /// <summary>
    ///     An event to invoke methods when exiting main scene
    /// </summary>
    public static event EventHandler<SceneEventArgs> OnExitMainScene;

    internal static void OnEnterMainSceneInvoke(int buildIndex, string sceneName) =>
        OnEnterMainScene?.Invoke(null, new SceneEventArgs(buildIndex, sceneName));

    internal static void OnExitMainSceneInvoke(int buildIndex, string sceneName) =>
        OnExitMainScene?.Invoke(null, new SceneEventArgs(buildIndex, sceneName));

    #endregion MainScene

    #region GameScene

    /// <summary>
    ///     Is in the Game Scene
    /// </summary>
    public static bool IsGameScene { get; internal set; }

    /// <summary>
    ///     An event to invoke methods when entering game scene
    /// </summary>
    public static event EventHandler<SceneEventArgs> OnEnterGameScene;

    /// <summary>
    ///     An event to invoke methods when exiting game scene
    /// </summary>
    public static event EventHandler<SceneEventArgs> OnExitGameScene;

    internal static void OnEnterGameSceneInvoke(int buildIndex, string sceneName) =>
        OnEnterGameScene?.Invoke(null, new SceneEventArgs(buildIndex, sceneName));

    internal static void OnExitGameSceneInvoke(int buildIndex, string sceneName) =>
        OnExitGameScene?.Invoke(null, new SceneEventArgs(buildIndex, sceneName));

    #endregion GameScene

    #region LoadingScene

    /// <summary>
    ///     Is in the loading scene
    /// </summary>
    public static bool IsLoadingScene { get; internal set; }

    /// <summary>
    ///     An event to invoke methods when entering loading scene
    /// </summary>
    public static event EventHandler<SceneEventArgs> OnEnterLoadingScene;

    /// <summary>
    ///     An event to invoke methods when exiting loading scene
    /// </summary>
    public static event EventHandler<SceneEventArgs> OnExitLoadingScene;

    internal static void OnEnterLoadingSceneInvoke(int buildIndex, string sceneName) =>
        OnEnterLoadingScene?.Invoke(null, new SceneEventArgs(buildIndex, sceneName));

    internal static void OnExitLoadingSceneInvoke(int buildIndex, string sceneName) =>
        OnExitLoadingScene?.Invoke(null, new SceneEventArgs(buildIndex, sceneName));

    #endregion LoadingScene

    #region WelcomeScene

    /// <summary>
    ///     Is in the Welcome Scene
    /// </summary>
    public static bool IsWelcomeScene { get; internal set; }

    /// <summary>
    ///     An event to invoke methods when entering welcome scene
    /// </summary>
    public static event EventHandler<SceneEventArgs> OnEnterWelcomeScene;

    /// <summary>
    ///     An event to invoke methods when exiting welcome scene
    /// </summary>
    public static event EventHandler<SceneEventArgs> OnExitWelcomeScene;

    internal static void OnEnterWelcomeSceneInvoke(int buildIndex, string sceneName) =>
        OnEnterWelcomeScene?.Invoke(null, new SceneEventArgs(buildIndex, sceneName));

    internal static void OnExitWelcomeSceneInvoke(int buildIndex, string sceneName) =>
        OnExitWelcomeScene?.Invoke(null, new SceneEventArgs(buildIndex, sceneName));

    #endregion
}