using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(PnlMenu), nameof(PnlMenu.Awake))]
internal static class PnlMenuPatch
{
    private static void Postfix(PnlMenu __instance)
    {
        GameObjectCache["PnlMenu"] = __instance.gameObject;
        GameObjectCache["PnlOption"] = __instance.transform.GetChild(2, 3).gameObject;

        PnlMenuPatchInvoke(__instance);
    }
}