using Assets.Scripts.UI.Panels;
using Assets.Scripts.UI.Specials;
using System;
using static MuseDashMirror.Utils;

namespace MuseDashMirror.CommonPatches
{
    public static class ManualPatches
    {
        /// <summary>
        /// Quick manual patch for pnlmenu
        /// </summary>
        public static void PnlMenuPatch(Type postfixOwnClass, string postfixMethodName) => PostfixPatch(typeof(PnlMenu), "Awake", postfixOwnClass, postfixMethodName);

        /// <summary>
        /// Quick manual patch for pnlstage
        /// </summary>
        public static void PnlStagePatch(Type postfixOwnClass, string postfixMethodName) => PostfixPatch(typeof(PnlStage), "Awake", postfixOwnClass, postfixMethodName);

        /// <summary>
        /// Quick manual patch for switchlanguages
        /// </summary>
        public static void SwitchLanguagesPatch(Type postfixOwnClass, string postfixMethodName) => PostfixPatch(typeof(SwitchLanguages), "OnClick", postfixOwnClass, postfixMethodName);
    }
}