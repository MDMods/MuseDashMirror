using Il2CppAssets.Scripts.GameCore;

namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(GameInit), nameof(GameInit.Awake))]
internal static class GameInitPatch
{
    private static void Postfix(GameInit __instance)
    {
        GameObjectCache["TglOn"] = __instance.transform.GetChild(1, 5, 3, 3, 0, 2, 0).gameObject;
        GameInitPatchInvoke(__instance);
    }
}
