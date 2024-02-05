namespace MuseDashMirror.EventArguments;

/// <summary>
///     Event arguments for the MenuSelect event.
/// </summary>
/// <param name="listIndex"></param>
/// <param name="index"></param>
/// <param name="isOn"></param>
public class MenuSelectEventArgs(int listIndex, int index, bool isOn) : EventArgs
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
    public int ListIndex { get; set; } = listIndex;

    /// <summary>
    ///     <list type="bullet">
    ///         <item>0 for Option</item>
    ///         <item>1 for Elfin</item>
    ///         <item>2 for Character</item>
    ///         <item>4 for Trove</item>
    ///         <item>8 for Achievement</item>
    ///     </list>
    /// </summary>
    public int Index { get; set; } = index;

    /// <summary>
    ///     Whether the menu item is on
    /// </summary>
    public bool IsOn { get; set; } = isOn;
}