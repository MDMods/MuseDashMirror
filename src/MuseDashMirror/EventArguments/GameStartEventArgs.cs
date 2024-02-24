using Il2CppFormulaBase;

namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event argument for <see cref="Il2CppFormulaBase.StageBattleComponent.GameStart" /> Patch
/// </summary>
/// <param name="stageBattleComponent"></param>
public sealed class GameStartEventArgs(StageBattleComponent stageBattleComponent) : EventArgs
{
    /// <summary>
    ///     <see cref="Il2CppFormulaBase.StageBattleComponent" /> Instance
    /// </summary>
    public StageBattleComponent StageBattleComponent => stageBattleComponent;
}