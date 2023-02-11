using Assets.Scripts.UI.Specials;
using MuseDashMirror.CommonPatches;

namespace MuseDashMirror.Patch
{
    [HarmonyPatch(typeof(SwitchLanguages), "OnClick")]
    internal static class SwitchLanguagesPatch
    {
        private static void Postfix()
        {
            PatchEvents.SwitchLanguagesEventInvoke();
        }
    }
}