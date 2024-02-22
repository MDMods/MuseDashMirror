using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event Argument for <see cref="Il2CppAssets.Scripts.UI.Panels.PnlStage" /> Patch
/// </summary>
/// <param name="pnlStage"></param>
public sealed class PnlStageEventArgs(PnlStage pnlStage) : EventArgs
{
    /// <summary>
    ///     PnlStage Instance
    /// </summary>
    public PnlStage PnlStage => pnlStage;
}