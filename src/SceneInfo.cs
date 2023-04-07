using System;

namespace MuseDashMirror;

/// <summary>
/// Scene load/unload events
/// </summary>
public static class SceneInfo
{
    #region MainScene

    /// <summary>
    /// Is in the Main Scene
    /// </summary>
    public static bool IsMainScene { get; internal set; }

    /// <summary>
    /// An event to invoke methods when entering main scene
    /// </summary>
    public static event Action EnterMainScene;

    /// <summary>
    /// An event to invoke methods when exiting main scene
    /// </summary>
    public static event Action ExitMainScene;

    internal static void EnterMainSceneInvoke() => EnterMainScene?.Invoke();

    internal static void ExitMainSceneInvoke() => ExitMainScene?.Invoke();

    #endregion MainScene

    #region GameScene

    /// <summary>
    /// Is in the Game Scene
    /// </summary>
    public static bool IsGameScene { get; internal set; }

    /// <summary>
    /// An event to invoke methods when entering game scene
    /// </summary>
    public static event Action EnterGameScene;

    /// <summary>
    /// An event to invoke methods when exiting game scene
    /// </summary>
    public static event Action ExitGameScene;

    internal static void EnterGameSceneInvoke() => EnterGameScene?.Invoke();

    internal static void ExitGameSceneInvoke() => ExitGameScene?.Invoke();

    #endregion GameScene

    #region LoadingScene

    /// <summary>
    /// Is in the loading scene
    /// </summary>
    public static bool IsLoadingScene { get; internal set; }

    /// <summary>
    /// An event to invoke methods when entering loading scene
    /// </summary>
    public static event Action EnterLoadingScene;

    /// <summary>
    /// An event to invoke methods when exiting loading scene
    /// </summary>
    public static event Action ExitLoadingScene;

    internal static void EnterLoadingSceneInvoke() => EnterLoadingScene?.Invoke();

    internal static void ExitLoadingSceneInvoke() => ExitLoadingScene?.Invoke();

    #endregion LoadingScene
}