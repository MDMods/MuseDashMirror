namespace MuseDashMirror.Models;

/// <summary>
///     Parameters for creating Toggle
/// </summary>
public class ToggleParameters
{
    /// <summary>
    ///     Toggle GameObject Name
    /// </summary>
    public string ToggleName { get; }

    /// <summary>
    ///     Text Parameters
    /// </summary>
    public TextParameters TextParameters { get; }

    /// <summary>
    ///     Toggle Group
    /// </summary>
    public ToggleGroup ToggleGroup { get; set; }

    /// <summary>
    ///     Initial Value
    /// </summary>
    public bool InitialValue { get; set; }

    /// <summary>
    ///     Boolean Callback
    /// </summary>
    public Action<bool> CallBack { get; set; }

    /// <summary>
    ///     Toggle CheckMark Color
    /// </summary>
    public Color CheckMarkColor { get; set; } = ToggleCheckMarkColor;

    /// <summary>
    ///     Create Toggle Parameters with Text Parameters and Boolean Callback
    /// </summary>
    /// <param name="toggleName">Toggle GameObject Name</param>
    /// <param name="textParameters">Text Parameters</param>
    /// <param name="initialValue">Initial Value</param>
    /// <param name="callBack">Boolean Callback</param>
    public ToggleParameters(string toggleName, TextParameters textParameters, bool initialValue, Action<bool> callBack)
    {
        ToggleName = toggleName;
        TextParameters = textParameters;
        InitialValue = initialValue;
        CallBack = callBack;

        if (textParameters.Color == Color.white)
        {
            textParameters.Color = ToggleTextColor;
        }
    }

    /// <summary>
    ///     Create Toggle Parameters with Text Parameters, Boolean Callback and Toggle Group
    /// </summary>
    /// <param name="toggleName">Toggle GameObject Name</param>
    /// <param name="textParameters">Text Parameters</param>
    /// <param name="initialValue">Initial Value</param>
    /// <param name="callBack">Boolean Callback</param>
    /// <param name="toggleGroup">Toggle Group</param>
    public ToggleParameters(string toggleName, TextParameters textParameters, bool initialValue, Action<bool> callBack, ToggleGroup toggleGroup)
        : this(toggleName, textParameters, initialValue, callBack) => ToggleGroup = toggleGroup;

    /// <summary>
    ///     Create Toggle Parameters with Text Parameters, Boolean Callback, Toggle Group and CheckMark Color
    /// </summary>
    /// <param name="toggleName">Toggle GameObject Name</param>
    /// <param name="textParameters">Text Parameters</param>
    /// <param name="initialValue">Initial Value</param>
    /// <param name="callBack">Boolean Callback</param>
    /// <param name="toggleGroup">Toggle Group</param>
    /// <param name="checkMarkColor">CheckMark Color</param>
    public ToggleParameters(string toggleName, TextParameters textParameters, bool initialValue, Action<bool> callBack, ToggleGroup toggleGroup,
        Color checkMarkColor) : this(toggleName, textParameters, initialValue, callBack, toggleGroup) => CheckMarkColor = checkMarkColor;
}