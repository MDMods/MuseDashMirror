using MelonLoader;
using static MuseDashMirror.UICreate.Fonts;
using static MuseDashMirror.AttributeProcessor;

namespace MuseDashMirror;

/// <summary>
///     Main class inherit from MelonMod
/// </summary>
public class Main : MelonMod
{
    /// <summary>
    ///     Load Fonts
    /// </summary>
    public override void OnInitializeMelon()
    {
        LoadFonts();
    }

    /// <summary>
    ///     Unload Fonts
    /// </summary>
    public override void OnDeinitializeMelon()
    {
        UnloadFonts();
    }

    /// <summary>
    ///     Scene load event
    /// </summary>
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        switch (sceneName)
        {
            case "GameMain":
                IsGameScene = true;
                OnEnterGameSceneInvoke(buildIndex, sceneName);
                break;

            case "UISystem_PC":
                IsMainScene = true;
                OnEnterMainSceneInvoke(buildIndex, sceneName);
                break;

            case "Loading":
                IsLoadingScene = true;
                OnEnterLoadingSceneInvoke(buildIndex, sceneName);
                break;

            case "Welcome":
                ProcessAttributeFromAssemblies();
                break;
        }
    }

    /// <summary>
    ///     Scene unload event
    /// </summary>
    public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
    {
        switch (sceneName)
        {
            case "GameMain":
                IsGameScene = false;
                OnExitGameSceneInvoke(buildIndex, sceneName);
                //ToggleCreate.Reset();
                BattleComponent.Reset();
                break;

            case "UISystem_PC":
                IsMainScene = false;
                OnExitMainSceneInvoke(buildIndex, sceneName);
                break;

            case "Loading":
                IsLoadingScene = false;
                OnExitLoadingSceneInvoke(buildIndex, sceneName);
                break;
        }
    }
}