using Il2CppAssets.Scripts.UI.Specials;

namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event Arguments for <see cref="Il2CppAssets.Scripts.UI.Specials.SwitchLanguages.OnClick" /> Patch
/// </summary>
/// <param name="switchLanguages"></param>
public sealed class SwitchLanguagesEventArgs(SwitchLanguages switchLanguages) : EventArgs
{
    /// <summary>
    ///     <see cref="Il2CppAssets.Scripts.UI.Specials.SwitchLanguages" /> Instance
    /// </summary>
    public SwitchLanguages SwitchLanguages => switchLanguages;
}