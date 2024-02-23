namespace MuseDashMirror.EventArguments;

public sealed class PnlVictoryEventArgs(PnlVictory pnlVictory) : EventArgs
{
    public PnlVictory PnlVictory => pnlVictory;
}