namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event Argument for <see cref="MenuSelect.OnToggleChanged" /> Patch
/// </summary>
/// <param name="listIndex"></param>
/// <param name="index"></param>
/// <param name="isOn"></param>
public sealed class MenuSelectEventArgs(int listIndex, int index, bool isOn) : EventArgs
{
    /// <summary>
    ///     <list type="bullet">
    ///         <item>0 for Option</item>
    ///         <item>1 for Elfin</item>
    ///         <item>2 for Character</item>
    ///         <item>3 for Trove</item>
    ///         <item>4 for Achievement</item>
    ///     </list>
    /// </summary>
    public int ListIndex => listIndex;

    /// <summary>
    ///     <list type="bullet">
    ///         <item>0 for Option</item>
    ///         <item>1 for Elfin</item>
    ///         <item>2 for Character</item>
    ///         <item>4 for Trove</item>
    ///         <item>8 for Achievement</item>
    ///     </list>
    /// </summary>
    public int Index => index;

    /// <summary>
    ///     Whether the menu item is on
    /// </summary>
    public bool IsOn => isOn;
}