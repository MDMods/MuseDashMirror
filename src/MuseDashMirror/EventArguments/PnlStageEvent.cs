using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.EventArguments;

public class PnlStageEventArgs(PnlStage __instance) : EventArgs
{
    public PnlStage PnlStage { get; set; } = __instance;
}