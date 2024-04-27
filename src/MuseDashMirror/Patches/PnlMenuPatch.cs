using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(PnlMenu), nameof(PnlMenu.Awake))]
internal static class PnlMenuPatch
{
    private static void Postfix(PnlMenu __instance)
    {
        GameObjectCache["PnlMenu"] = __instance.gameObject;
        GameObjectCache["PnlOption"] = __instance.transform.GetChildGameObject(2, 3);

        PnlMenuPatchInvoke(__instance);
    }
}