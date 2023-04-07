using System;
using Assets.Scripts.UI.Panels;

namespace MuseDashMirror.CommonPatches;

/// <summary>
/// Common events for patching
/// </summary>
public static class PatchEvents
{
    /// <summary>
    /// An event to invoke methods when PnlMenu Awake method invokes
    /// </summary>
    public static event Action<PnlMenu> PnlMenuEvent;

    internal static void PnlMenuEventInvoke(PnlMenu pnlMenu) => PnlMenuEvent?.Invoke(pnlMenu);

    /// <summary>
    /// An event to invoke methods when PnlStage Awake method invokes
    /// </summary>
    public static event Action<PnlStage> PnlStageEvent;

    internal static void PnlStageEventInvoke(PnlStage pnlStage) => PnlStageEvent?.Invoke(pnlStage);

    /// <summary>
    /// An event to invoke methods when switching languages
    /// </summary>
    public static event Action SwitchLanguagesEvent;

    internal static void SwitchLanguagesEventInvoke() => SwitchLanguagesEvent?.Invoke();

    /// <summary>
    /// An event to invoke methods when switching menu
    /// </summary>
    public static event Action<int, int, bool> MenuSelectEvent;

    internal static void MenuSelectEventInvoke(int listIndex, int index, bool isOn) => MenuSelectEvent?.Invoke(listIndex, index, isOn);
}