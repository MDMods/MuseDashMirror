using Il2CppAssets.Scripts.GameCore.HostComponent;

namespace MuseDashMirror.EventArguments;

public sealed class TaskStageTargetEventArgs(TaskStageTarget taskStageTarget) : EventArgs
{
    public TaskStageTarget TaskStageTarget => taskStageTarget;
}