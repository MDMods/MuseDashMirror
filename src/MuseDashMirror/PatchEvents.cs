using Il2CppAssets.Scripts.GameCore;
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
    ///     An event to invoke methods when <see cref="PnlMenu" />'s <see cref="PnlMenu.Awake" /> method invokes
    /// </summary>
    public static event EventHandler<PnlMenuEventArgs> PnlMenuPatch;

    internal static void PnlMenuPatchInvoke(PnlMenu pnlMenu) =>
        PnlMenuPatch?.Invoke(null, new PnlMenuEventArgs(pnlMenu));

    /// <summary>
    ///     An event to invoke methods when <see cref="PnlStage" />'s <see cref="PnlStage.Awake" /> method invokes
    /// </summary>
    public static event EventHandler<PnlStageEventArgs> PnlStagePatch;

    internal static void PnlStagePatchInvoke(PnlStage pnlStage) =>
        PnlStagePatch?.Invoke(null, new PnlStageEventArgs(pnlStage));

    /// <summary>
    ///     An event to invoke methods when <see cref="SwitchLanguages" />'s <see cref="SwitchLanguages.OnClick" /> method invokes
    /// </summary>
    public static event EventHandler SwitchLanguagesPatch;

    internal static void SwitchLanguagesPatchInvoke(SwitchLanguages switchLanguages) =>
        SwitchLanguagesPatch?.Invoke(null, new SwitchLanguagesEventArgs(switchLanguages));

    /// <summary>
    ///     An event to invoke methods when <see cref="MenuSelect" />'s <see cref="MenuSelect.OnToggleChanged" /> method invokes
    /// </summary>
    public static event EventHandler<MenuSelectEventArgs> MenuSelectPatch;

    internal static void MenuSelectInvoke(int listIndex, int index, bool isOn) =>
        MenuSelectPatch?.Invoke(null, new MenuSelectEventArgs(listIndex, index, isOn));

    /// <summary>
    ///     An event to invoke methods when <see cref="PnlVictory" />'s
    ///     <see
    ///         cref="PnlVictory.OnVictory(Il2CppSystem.Object,Il2CppSystem.Object,Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray{Il2CppSystem.Object})" />
    ///     method invokes
    /// </summary>
    public static event EventHandler<PnlVictoryEventArgs> PnlVictoryPatch;

    internal static void PnlVictoryPatchInvoke(PnlVictory pnlVictory) =>
        PnlVictoryPatch?.Invoke(null, new PnlVictoryEventArgs(pnlVictory));

    /// <summary>
    ///     An event to invoke methods when <see cref="StageBattleComponent" />'s <see cref="StageBattleComponent.GameStart" /> method invokes
    /// </summary>
    public static event EventHandler<GameStartEventArgs> GameStartPatch;

    internal static void GameStartPatchInvoke(StageBattleComponent stageBattleComponent) =>
        GameStartPatch?.Invoke(null, new GameStartEventArgs(stageBattleComponent));

    /// <summary>
    ///     An event to invoke methods when <see cref="TaskStageTarget" />'s <see cref="TaskStageTarget.AddScore" /> method invokes
    /// </summary>
    public static event EventHandler<AddScoreEventArgs> AddScorePatch;

    internal static void AddScorePatchInvoke(TaskStageTarget taskStageTarget, int value, int id, string noteType, bool isAir, float time = -1f) =>
        AddScorePatch?.Invoke(null, new AddScoreEventArgs(taskStageTarget, value, id, noteType, isAir, time));

    /// <summary>
    ///     An event to invoke methods when <see cref="GameInit" />'s <see cref="GameInit.Awake" /> method invokes
    /// </summary>
    public static event EventHandler<GameInitEventArgs> GameInitPatch;

    internal static void GameInitPatchInvoke(GameInit gameInit) =>
        GameInitPatch?.Invoke(null, new GameInitEventArgs(gameInit));
}