using Il2CppAssets.Scripts.UI.Panels;

namespace MuseDashMirror.Patch;

/// <summary>
///     Common events for patching
/// </summary>
public static class PatchEvents
{
    /// <summary>
    ///     An event to invoke methods when PnlMenu Awake method invokes
    /// </summary>
    public static event EventHandler<PnlMenuEventArgs> PnlMenuEvent;

    internal static void PnlMenuEventInvoke(PnlMenu __instance) => PnlMenuEvent?.Invoke(null, new PnlMenuEventArgs(__instance));

    /// <summary>
    ///     An event to invoke methods when PnlStage Awake method invokes
    /// </summary>
    public static event EventHandler<PnlStageEventArgs> PnlStageEvent;

    internal static void PnlStageEventInvoke(PnlStage __instance) => PnlStageEvent?.Invoke(null, new PnlStageEventArgs(__instance));

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