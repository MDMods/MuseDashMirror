using Il2CppFormulaBase;

namespace MuseDashMirror.EventArguments;

public sealed class StageBattleComponentEventArgs(StageBattleComponent stageBattleComponent) : EventArgs
{
    public StageBattleComponent StageBattleComponent => stageBattleComponent;
}