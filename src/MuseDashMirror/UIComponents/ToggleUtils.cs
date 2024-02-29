using System.Linq.Expressions;
using Il2CppAssets.Scripts.PeroTools.GeneralLocalization;
using Il2CppAssets.Scripts.PeroTools.Nice.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace MuseDashMirror.UIComponents;

/// <summary>
///     Methods for creating toggle
/// </summary>
public static partial class ToggleUtils
{
    private const string TglOnPath = "Forward/PnlVolume/LogoSetting/Toggles/TglOn";

    private static readonly Vector3[] Positions =
    [
        new Vector3(-6.8f, -2.55f, 100f),
        new Vector3(-6.8f, -3.35f, 100f),
        new Vector3(-6.8f, -4.15f, 100f),
        new Vector3(-6.8f, -4.95f, 100f)
    ];

    private static int ToggleCount { get; set; }

    private static List<float> LongestWidths { get; } = [];

    /// <summary>
    ///     Create a toggle in the PnlMenu
    /// </summary>
    /// <param name="name">GameObject Name</param>
    /// <param name="text">Toggle Text</param>
    /// <param name="callback">Boolean Callback</param>
    /// <returns>Toggle GameObject</returns>
    public static GameObject CreatePnlMenuToggle(string name, string text, Action<bool> callback)
    {
        var toggle = Object.Instantiate(GetGameObject(TglOnPath), GetGameObject("PnlMenu").transform);
        toggle.name = name;

        var txt = toggle.transform.GetChild(1).gameObject;
        txt.GetComponent<Localization>().Destroy();
        txt.SetText(text);
        txt.AddContentSizeFitter();

        var rectTransform = txt.GetComponent<RectTransform>();
        rectTransform.UpdateTransformLayoutInfo();

        toggle.transform.position = GetPosition(rectTransform);

        toggle.GetComponent<OnToggle>().Destroy();
        toggle.GetComponent<OnToggleOn>().Destroy();
        toggle.GetComponent<OnActivate>().Destroy();

        var toggleComp = toggle.GetComponent<Toggle>();
        toggleComp.onValueChanged.AddListener(callback);
        toggleComp.group = null;

        toggle.SetParent(GetGameObject("PnlOption"));

        return toggle;
    }

    /// <summary>
    ///     Create a toggle in the PnlMenu
    /// </summary>
    /// <param name="name">GameObject Name</param>
    /// <param name="text">Toggle Text</param>
    /// <param name="target">Target Object the Toggle's value will be bounded to</param>
    /// <param name="expression">An expression that represents a Boolean property of the Target Object</param>
    /// <typeparam name="T">The type of the Target Object</typeparam>
    /// <returns>Toggle GameObject</returns>
    /// <exception cref="ArgumentException">Thrown when the provided expression does not represent a property or field of the target object</exception>
    public static GameObject CreatePnlMenuToggle<T>(string name, string text, T target, Expression<Func<T, bool>> expression)
    {
        var toggle = Object.Instantiate(GetGameObject(TglOnPath), GetGameObject("PnlMenu").transform);
        toggle.name = name;

        var txt = toggle.transform.GetChild(1).gameObject;
        txt.GetComponent<Localization>().Destroy();
        txt.SetText(text);
        txt.AddContentSizeFitter();

        var rectTransform = txt.GetComponent<RectTransform>();
        rectTransform.UpdateTransformLayoutInfo();

        toggle.transform.position = GetPosition(rectTransform);

        if (expression.Body is not MemberExpression memberExpression)
        {
            throw new ArgumentException("expression must return a field or property");
        }

        var parameterExpression = Expression.Parameter(typeof(bool));
        var setter = Expression.Lambda<Action<T, bool>>(
            Expression.Assign(memberExpression, parameterExpression),
            Expression.Parameter(typeof(T)),
            parameterExpression).Compile();

        toggle.GetComponent<OnToggle>().Destroy();
        toggle.GetComponent<OnToggleOn>().Destroy();
        toggle.GetComponent<OnActivate>().Destroy();

        var toggleComp = toggle.GetComponent<Toggle>();
        toggleComp.onValueChanged.AddListener((Action<bool>)(val => setter(target, val)));
        toggleComp.group = null;

        toggle.SetParent(GetGameObject("PnlOption"));

        return toggle;
    }

    /// <summary>
    ///     Create a toggle in the PnlMenu
    /// </summary>
    /// <param name="name">GameObject Name</param>
    /// <param name="toggleParameters">Toggle Parameters</param>
    /// <returns>Toggle GameObject</returns>
    public static GameObject CreatePnlMenuToggle(string name, ToggleParameters toggleParameters)
    {
        var toggle = Object.Instantiate(GetGameObject(TglOnPath), GetGameObject("PnlMenu").transform);
        toggle.name = name;

        var txt = toggle.transform.GetChild(1).gameObject;
        txt.GetComponent<Localization>().Destroy();
        txt.SetTextComponent(toggleParameters.TextParameters);
        txt.AddContentSizeFitter();

        var rectTransform = txt.GetComponent<RectTransform>();
        rectTransform.UpdateTransformLayoutInfo();

        toggle.transform.position = GetPosition(rectTransform);

        toggle.GetComponent<OnToggle>().Destroy();
        toggle.GetComponent<OnToggleOn>().Destroy();
        toggle.GetComponent<OnActivate>().Destroy();

        var toggleComp = toggle.GetComponent<Toggle>();
        toggleComp.onValueChanged.AddListener(toggleParameters.CallBack);
        toggleComp.group = toggleParameters.ToggleGroup;

        var checkMark = toggle.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        checkMark.color = toggleParameters.CheckMarkColor;

        toggle.SetParent(GetGameObject("PnlOption"));
        return toggle;
    }

    private static Vector3 GetPosition(RectTransform rectTransform)
    {
        var scaledFactor = rectTransform.gameObject.GetTotalScaledFactor();
        var width = rectTransform.rect.width * scaledFactor + 1.1f;
        var column = ToggleCount / 4;

        if (LongestWidths.Count <= column)
        {
            LongestWidths.Add(0f);
        }

        if (width > LongestWidths[column])
        {
            LongestWidths[column] = width;
        }

        var position = Positions[ToggleCount % 4];

        if (column > 0)
        {
            position.x += LongestWidths.Take(column).Sum();
        }

        ToggleCount++;
        return position;
    }

    [ExitMainScene]
    private static void Reset(object e, SceneEventArgs args) => ToggleCount = 0;
}