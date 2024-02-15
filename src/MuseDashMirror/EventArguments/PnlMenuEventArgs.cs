using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.EventArguments;

public class PnlMenuEventArgs(PnlMenu pnlMenu) : EventArgs
{
    public PnlMenu PnlMenu { get; set; } = pnlMenu;
}