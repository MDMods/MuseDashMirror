using System;

namespace MuseDashMirror;

public static class SceneInfo
{
    #region MainScene

    internal static bool isMainScene { get; set; }
    public static bool IsMainScene => isMainScene;

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

    internal static bool isGameScene { get; set; }
    public static bool IsGameScene => isGameScene;

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

    internal static bool isLoadingScene { get; set; }
    public static bool IsLoadingScene => isLoadingScene;

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