using Il2CppAssets.Scripts.GameCore;

namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(GameInit), nameof(GameInit.Awake))]
internal static class GameInitPatch
{
    [UsedImplicitly]
    private static void Postfix(GameInit __instance)
    {
        GameObjectCache["UI"] = __instance.gameObject;
        GameObjectCache["TglOn"] = __instance.transform.GetChild(2, 5, 7, 2, 0).gameObject;
        GameInitPatchInvoke(__instance);
    }
}