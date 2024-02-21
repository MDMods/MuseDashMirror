using Il2CppAssets.Scripts.UI.Specials;

namespace MuseDashMirror.Patch;

[HarmonyPatch(typeof(SwitchLanguages), nameof(SwitchLanguages.OnClick))]
internal static class SwitchLanguagesPatch
{
    private static void Postfix(SwitchLanguages __instance) => SwitchLanguagesPatchInvoke(__instance);
}