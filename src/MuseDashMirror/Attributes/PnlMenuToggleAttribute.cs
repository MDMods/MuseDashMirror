namespace MuseDashMirror.Attributes;

/// <summary>
///     Attribute for creating a toggle in the PnlMenu
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class PnlMenuToggleAttribute : Attribute
{
    /// <summary>
    ///     Attribute for creating a toggle in the PnlMenu
    /// </summary>
    /// <param name="name">Toggle GameObject Name</param>
    /// <param name="text">Toggle Text</param>
    /// <param name="boolMemberName">Bounded static Boolean member name</param>
    public PnlMenuToggleAttribute(string name, string text, string boolMemberName) { }

    /// <summary>
    ///     Attribute for creating a toggle in the PnlMenu as part of a toggle group
    /// </summary>
    /// <param name="name">Toggle GameObject Name</param>
    /// <param name="text">Toggle Text</param>
    /// <param name="boolMemberName">Bounded static Boolean member name</param>
    /// <param name="toggleGroupMemberName">Static ToggleGroup member name</param>
    public PnlMenuToggleAttribute(string name, string text, string boolMemberName, string toggleGroupMemberName) { }
}
