﻿using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(PnlMenu), nameof(PnlMenu.Awake))]
internal static class PnlMenuPatch
{
    private static void Postfix(PnlMenu __instance)
    {
        if (!GameObjectCache.ContainsKey("TglOn"))
        {
            GameObjectCache["TglOn"] = GameObject.Find("Forward").transform.Find("PnlVolume").Find("LogoSetting").GetChild(2).GetChild(0).gameObject;
        }

        if (!GameObjectCache.ContainsKey("PnlMenu"))
        {
            GameObjectCache["PnlMenu"] = __instance.gameObject;
        }

        PnlMenuPatchInvoke(__instance);
    }
}