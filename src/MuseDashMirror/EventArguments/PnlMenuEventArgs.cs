using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event Argument for <see cref="Il2CppAssets.Scripts.UI.Panels.PnlMenu" /> Patch
/// </summary>
/// <param name="pnlMenu"></param>
public sealed class PnlMenuEventArgs(PnlMenu pnlMenu) : EventArgs
{
    /// <summary>
    ///     PnlMenu Instance
    /// </summary>
    public PnlMenu PnlMenu => pnlMenu;
}