using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.EventArguments;

public sealed class PnlStageEventArgs(PnlStage pnlStage) : EventArgs
{
    public PnlStage PnlStage { get; set; } = pnlStage;
}