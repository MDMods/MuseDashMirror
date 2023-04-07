global using HarmonyLib;
using MelonLoader;
using MuseDashMirror.UICreate;
using static MuseDashMirror.SceneInfo;
using static MuseDashMirror.UICreate.Fonts;

namespace MuseDashMirror;

/// <summary>
/// Main class inherit from MelonMod
/// </summary>
public class Main : MelonMod
{
    internal static HarmonyLib.Harmony harmony { get; set; }

    /// <summary>
    /// Load Fonts
    /// </summary>
    public override void OnInitializeMelon()
    {
        harmony = HarmonyInstance;
        LoadFonts();
    }

    /// <summary>
    /// Unload Fonts
    /// </summary>
    public override void OnDeinitializeMelon()
    {
        UnloadFonts();
    }

    /// <summary>
    /// Scene load event
    /// </summary>
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        switch (sceneName)
        {
            case "GameMain":
                IsGameScene = true;
                EnterGameSceneInvoke();
                break;

            case "UISystem_PC":
                IsMainScene = true;
                EnterMainSceneInvoke();
                break;

            case "Loading":
                IsLoadingScene = true;
                EnterLoadingSceneInvoke();
                break;
        }
    }

    /// <summary>
    /// Scene unload event
    /// </summary>
    public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
    {
        switch (sceneName)
        {
            case "GameMain":
                IsGameScene = false;
                ExitGameSceneInvoke();
                ToggleCreate.Reset();
                BattleComponent.Reset();
                break;

            case "UISystem_PC":
                IsMainScene = false;
                ExitMainSceneInvoke();
                break;

            case "Loading":
                IsLoadingScene = false;
                ExitLoadingSceneInvoke();
                break;
        }
    }
}