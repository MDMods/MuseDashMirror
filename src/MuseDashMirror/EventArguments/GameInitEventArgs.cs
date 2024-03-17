using Il2CppAssets.Scripts.GameCore;

namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event argument for <see cref="Il2CppAssets.Scripts.GameCore.GameInit" /> Patch
/// </summary>
/// <param name="gameInit"></param>
public sealed class GameInitEventArgs(GameInit gameInit) : EventArgs
{
    /// <summary>
    ///     <see cref="Il2CppAssets.Scripts.GameCore.GameInit" /> Instance
    /// </summary>
    public GameInit GameInit => gameInit;
}