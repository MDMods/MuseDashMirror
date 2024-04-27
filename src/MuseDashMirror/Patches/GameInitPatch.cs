using Il2CppAssets.Scripts.GameCore;

namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(GameInit), nameof(GameInit.Awake))]
internal static class GameInitPatch
{
    private static void Postfix(GameInit __instance)
    {
        GameObjectCache["UI"] = __instance.gameObject;
        GameObjectCache["Forward"] = __instance.transform.GetChildGameObject(2);
        GameObjectCache["TglOn"] = __instance.transform.GetChildTransform(2, 5, 7, 2, 0).gameObject;
        GameInitPatchInvoke(__instance);
    }
}