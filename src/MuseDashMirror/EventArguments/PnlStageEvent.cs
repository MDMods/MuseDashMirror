using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.EventArguments;

public class PnlStageEventArgs(PnlStage pnlStage) : EventArgs
{
    public PnlStage PnlStage { get; set; } = pnlStage;
}