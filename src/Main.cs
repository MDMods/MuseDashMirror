global using HarmonyLib;
using MelonLoader;
using MuseDashMirror.UICreate;
using static MuseDashMirror.SceneInfo;

namespace MuseDashMirror;

public class Main : MelonMod
{
    internal static HarmonyLib.Harmony harmony { get; set; }

    public override void OnInitializeMelon()
    {
        harmony = HarmonyInstance;
    }

    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        switch (sceneName)
        {
            case "GameMain":
                isGameScene = true;
                EnterGameSceneInvoke();
                break;

            case "UISystem_PC":
                isMainScene = true;
                EnterMainSceneInvoke();
                break;

            case "Loading":
                isLoadingScene = true;
                EnterLoadingSceneInvoke();
                break;
        }
    }

    public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
    {
        switch (sceneName)
        {
            case "GameMain":
                isGameScene = false;
                ExitGameSceneInvoke();
                ToggleCreate.Reset();
                BattleComponent.Reset();
                break;

            case "UISystem_PC":
                isMainScene = false;
                ExitMainSceneInvoke();
                break;

            case "Loading":
                isLoadingScene = false;
                ExitLoadingSceneInvoke();
                break;
        }
    }
}