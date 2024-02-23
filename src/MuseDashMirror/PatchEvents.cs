using Il2CppAssets.Scripts.GameCore.HostComponent;
using Il2CppAssets.Scripts.UI.Panels;
using Il2CppAssets.Scripts.UI.Specials;
using Il2CppFormulaBase;

namespace MuseDashMirror;

/// <summary>
///     Common events for patching
/// </summary>
public static class PatchEvents
{
    /// <summary>
    ///     An event to invoke methods when PnlMenu Awake method invokes
    /// </summary>
    public static event EventHandler<PnlMenuEventArgs> PnlMenuPatch;

    internal static void PnlMenuPatchInvoke(PnlMenu pnlMenu) =>
        PnlMenuPatch?.Invoke(null, new PnlMenuEventArgs(pnlMenu));

    /// <summary>
    ///     An event to invoke methods when PnlStage Awake method invokes
    /// </summary>
    public static event EventHandler<PnlStageEventArgs> PnlStagePatch;

    internal static void PnlStagePatchInvoke(PnlStage pnlStage) =>
        PnlStagePatch?.Invoke(null, new PnlStageEventArgs(pnlStage));

    /// <summary>
    ///     An event to invoke methods when switching languages
    /// </summary>
    public static event EventHandler SwitchLanguagesPatch;

    internal static void SwitchLanguagesPatchInvoke(SwitchLanguages switchLanguages) =>
        SwitchLanguagesPatch?.Invoke(null, new SwitchLanguagesEventArgs(switchLanguages));

    /// <summary>
    ///     An event to invoke methods when switching menu
    /// </summary>
    public static event EventHandler<MenuSelectEventArgs> MenuSelectPatch;

    internal static void MenuSelectInvoke(int listIndex, int index, bool isOn) =>
        MenuSelectPatch?.Invoke(null, new MenuSelectEventArgs(listIndex, index, isOn));

    /// <summary>
    ///     An event to invoke methods when PnlVictory's OnVictory method invokes
    /// </summary>
    public static event EventHandler<PnlVictoryEventArgs> PnlVictoryPatch;

    internal static void PnlVictoryPatchInvoke(PnlVictory pnlVictory) =>
        PnlVictoryPatch?.Invoke(null, new PnlVictoryEventArgs(pnlVictory));

    /// <summary>
    ///     An event to invoke methods when StageBattleComponent's GameStart method invokes
    /// </summary>
    public static event EventHandler<StageBattleComponentEventArgs> StageBattleComponentPatch;

    internal static void StageBattleComponentPatchInvoke(StageBattleComponent stageBattleComponent) =>
        StageBattleComponentPatch?.Invoke(null, new StageBattleComponentEventArgs(stageBattleComponent));

    /// <summary>
    ///     An event to invoke methods when TaskStageTarget's AddScore method invokes
    /// </summary>
    public static event EventHandler<TaskStageTargetEventArgs> TaskStageTargetPatch;

    internal static void TaskStageTargetPatchInvoke(TaskStageTarget taskStageTarget) =>
        TaskStageTargetPatch?.Invoke(null, new TaskStageTargetEventArgs(taskStageTarget));
}