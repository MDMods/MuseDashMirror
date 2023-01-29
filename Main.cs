global using HarmonyLib;
using MelonLoader;
using static MuseDashMirror.SceneInfo;
using static MuseDashMirror.UICreate;

namespace MuseDashMirror
{
    public class Main : MelonMod
    {
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName == "GameMain")
            {
                IsGameScene = true;
            }
            else
            {
                IsGameScene = false;
            }

            if (sceneName == "UISystem_PC")
            {
                IsMainScene = true;
            }
            else
            {
                IsMainScene = false;
            }

            if (sceneName == "Loading")
            {
                IsLoadingScene = true;
            }
            else
            {
                IsLoadingScene = false;
            }
        }
    }
}