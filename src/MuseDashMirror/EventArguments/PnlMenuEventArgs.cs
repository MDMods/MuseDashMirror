using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event Argument for <see cref="Il2CppAssets.Scripts.UI.Panels.PnlMenu.Awake" /> Patch
/// </summary>
/// <param name="pnlMenu"></param>
public sealed class PnlMenuEventArgs(PnlMenu pnlMenu) : EventArgs
{
    /// <summary>
    ///     <see cref="Il2CppAssets.Scripts.UI.Panels.PnlMenu" /> Instance
    /// </summary>
    public PnlMenu PnlMenu => pnlMenu;
}