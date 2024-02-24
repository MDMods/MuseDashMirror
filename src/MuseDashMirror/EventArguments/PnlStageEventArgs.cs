using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event Argument for <see cref="Il2CppAssets.Scripts.UI.Panels.PnlStage.Awake" /> Patch
/// </summary>
/// <param name="pnlStage"></param>
public sealed class PnlStageEventArgs(PnlStage pnlStage) : EventArgs
{
    /// <summary>
    ///     <see cref="Il2CppAssets.Scripts.UI.Panels.PnlStage" /> Instance
    /// </summary>
    public PnlStage PnlStage => pnlStage;
}