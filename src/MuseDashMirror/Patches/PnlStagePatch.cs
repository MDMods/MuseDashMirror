using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.Patches;

[HarmonyPatch(typeof(PnlStage), nameof(PnlStage.Awake))]
internal static class PnlStagePatch
{
    private static void Postfix(PnlStage __instance)
    {
        if (!GameObjectCache.ContainsKey("TglOn"))
        {
            GameObjectCache["TglOn"] = __instance.transform.parent.parent.GetChild(2, 5, 7, 2, 0).gameObject;
        }

        PnlStagePatchInvoke(__instance);
    }
}