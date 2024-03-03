using Il2CppAssets.Scripts.GameCore.HostComponent;

namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event argument for <see cref="Il2CppAssets.Scripts.GameCore.HostComponent.TaskStageTarget.AddScore" /> Patch
/// </summary>
/// <param name="taskStageTarget"></param>
/// <param name="value"></param>
/// <param name="id"></param>
/// <param name="noteType"></param>
/// <param name="isAir"></param>
/// <param name="time"></param>
public sealed class AddScoreEventArgs(TaskStageTarget taskStageTarget, int value, int id, string noteType, bool isAir, float time = -1f) : EventArgs
{
    /// <summary>
    ///     <see cref="Il2CppAssets.Scripts.GameCore.HostComponent.TaskStageTarget" /> Instance
    /// </summary>
    public TaskStageTarget TaskStageTarget => taskStageTarget;

    /// <summary>
    ///     Score Value
    /// </summary>
    public int Value => value;

    /// <summary>
    ///     Note Id
    /// </summary>
    public int Id => id;

    /// <summary>
    ///     Note Type
    /// </summary>
    public string NoteType => noteType;

    /// <summary>
    ///     Is Air Note
    /// </summary>
    public bool IsAir => isAir;

    /// <summary>
    ///     Time
    /// </summary>
    public float Time => time;
}