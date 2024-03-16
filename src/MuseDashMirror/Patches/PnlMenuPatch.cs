using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(PnlMenu), nameof(PnlMenu.Awake))]
internal static class PnlMenuPatch
{
    private static void Postfix(PnlMenu __instance)
    {
        if (!GameObjectCache.ContainsKey("TglOn"))
        {
            GameObjectCache["TglOn"] = __instance.transform.GetAncestorAtLevel(2).GetChild(2, 5, 7, 2, 0).gameObject;
        }

        GameObjectCache["PnlMenu"] = __instance.gameObject;
        GameObjectCache["PnlOption"] = __instance.transform.GetChild(2).GetChild(3).gameObject;

        PnlMenuPatchInvoke(__instance);
    }
}