global using HarmonyLib;
using MelonLoader;
using static MuseDashMirror.SceneInfo;

namespace MuseDashMirror
{
    public class Main : MelonMod
    {
        internal static HarmonyLib.Harmony harmony { get; set; }

        public override void OnInitializeMelon()
        {
            harmony = HarmonyInstance;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName == "GameMain")
            {
                isGameScene = true;
                EnterGameSceneInvoke();
            }

            if (sceneName == "UISystem_PC")
            {
                isMainScene = true;
                EnterMainSceneInvoke();
            }

            if (sceneName == "Loading")
            {
                isLoadingScene = true;
                EnterLoadingSceneInvoke();
            }
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            if (sceneName == "GameMain")
            {
                isGameScene = false;
                ExitGameSceneInvoke();
            }

            if (sceneName == "UISystem_PC")
            {
                isMainScene = false;
                ExitMainSceneInvoke();
            }

            if (sceneName == "Loading")
            {
                isLoadingScene = false;
                ExitLoadingSceneInvoke();
            }
        }
    }
}