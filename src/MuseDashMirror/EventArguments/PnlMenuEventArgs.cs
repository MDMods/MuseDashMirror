using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.EventArguments;

public class PnlMenuEventArgs(PnlMenu __instance) : EventArgs
{
    public PnlMenu PnlMenu { get; set; } = __instance;
}