namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event Argument for
///     <see
///         cref="PnlVictory.OnVictory(Il2CppSystem.Object,Il2CppSystem.Object,Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray{Il2CppSystem.Object})" />
///     Patch
/// </summary>
/// <param name="pnlVictory"></param>
public sealed class PnlVictoryEventArgs(PnlVictory pnlVictory) : EventArgs
{
    /// <summary>
    ///     <see cref="Il2Cpp.PnlVictory" /> Instance
    /// </summary>
    public PnlVictory PnlVictory => pnlVictory;
}