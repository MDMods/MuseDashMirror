using Il2CppAssets.Scripts.UI.Specials;

namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event Arguments for <see cref="Il2CppAssets.Scripts.UI.Specials.SwitchLanguages" /> Patch
/// </summary>
/// <param name="switchLanguages"></param>
public sealed class SwitchLanguagesEventArgs(SwitchLanguages switchLanguages) : EventArgs
{
    /// <summary>
    ///     SwitchLanguages Instance
    /// </summary>
    public SwitchLanguages SwitchLanguages => switchLanguages;
}