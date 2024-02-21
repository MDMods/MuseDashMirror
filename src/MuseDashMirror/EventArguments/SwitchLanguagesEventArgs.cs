using Il2CppAssets.Scripts.UI.Specials;

namespace MuseDashMirror.EventArguments;

public sealed class SwitchLanguagesEventArgs(SwitchLanguages switchLanguages) : EventArgs
{
    public SwitchLanguages SwitchLanguages { get; } = switchLanguages;
}