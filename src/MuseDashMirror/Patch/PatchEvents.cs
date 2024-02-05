using MuseDashMirror.EventArguments;

namespace MuseDashMirror.Patch;

/// <summary>
///     Common events for patching
/// </summary>
public static class PatchEvents
{
    /// <summary>
    ///     An event to invoke methods when PnlMenu Awake method invokes
    /// </summary>
    public static event EventHandler PnlMenuEvent;

    internal static void PnlMenuEventInvoke() => PnlMenuEvent?.Invoke(null, EventArgs.Empty);

    /// <summary>
    ///     An event to invoke methods when PnlStage Awake method invokes
    /// </summary>
    public static event EventHandler PnlStageEvent;

    internal static void PnlStageEventInvoke() => PnlStageEvent?.Invoke(null, EventArgs.Empty);

    /// <summary>
    ///     An event to invoke methods when switching languages
    /// </summary>
    public static event EventHandler SwitchLanguagesEvent;

    internal static void SwitchLanguagesEventInvoke() => SwitchLanguagesEvent?.Invoke(null, EventArgs.Empty);

    /// <summary>
    ///     An event to invoke methods when switching menu
    /// </summary>
    public static event EventHandler<MenuSelectEventArgs> MenuSelectEvent;

    internal static void MenuSelectEventInvoke(int listIndex, int index, bool isOn) =>
        MenuSelectEvent?.Invoke(null, new MenuSelectEventArgs(listIndex, index, isOn));
}