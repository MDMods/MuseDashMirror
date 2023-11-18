using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(PnlMenu), nameof(PnlMenu.Awake))]
internal static class PnlMenuPatch
{
    private static void Postfix() => PnlMenuEventInvoke();
}