using System.Linq.Expressions;
using Il2CppAssets.Scripts.PeroTools.GeneralLocalization;
using Il2CppAssets.Scripts.PeroTools.Nice.Events;

namespace MuseDashMirror.UIComponents;

/// <summary>
///     Methods for creating toggle
/// </summary>
public static partial class ToggleUtils
{
    private const float ToggleSpacing = 1.1f;

    private static readonly Vector3[] Positions =
    [
        new Vector3(-6.8f, -2.55f, 100f),
        new Vector3(-6.8f, -3.35f, 100f),
        new Vector3(-6.8f, -4.15f, 100f),
        new Vector3(-6.8f, -4.95f, 100f)
    ];

    private static float ScaleFactorX => PnlMenuGameObject.GetTotalScaleFactor().x;
    private static GameObject TglOnGameObject => GetGameObjectWithPath("TglOn", true);
    private static GameObject PnlMenuGameObject => GetGameObjectWithPath("PnlMenu", true);
    private static GameObject PnlOptionGameObject => GetGameObjectWithPath("PnlOption", true);
    private static int ToggleCount { get; set; }
    private static List<float> LongestWidths { get; } = [];

    /// <summary>
    ///     Create a toggle
    /// </summary>
    /// <param name="toggleParameters">Toggle Parameters</param>
    /// <param name="transformParameters">Transform Parameters</param>
    /// <returns></returns>
    public static GameObject CreateToggle(ToggleParameters toggleParameters, TransformParameters transformParameters) =>
        CreateToggle((Transform)null, toggleParameters, transformParameters);

    /// <summary>
    ///     Create a toggle with parent
    /// </summary>
    /// <param name="parentName">Parent GameObject Name</param>
    /// <param name="toggleParameters">Toggle Parameters</param>
    /// <param name="transformParameters">Transform Parameters</param>
    /// <returns>Toggle GameObject</returns>
    public static GameObject CreateToggle(string parentName, ToggleParameters toggleParameters, TransformParameters transformParameters)
        => CreateToggle(GetGameObjectWithPath(parentName), toggleParameters, transformParameters);

    /// <summary>
    ///     Create a toggle with parent
    /// </summary>
    /// <param name="parent">Parent GameObject</param>
    /// <param name="toggleParameters">Toggle Parameters</param>
    /// <param name="transformParameters">Transform Parameters</param>
    /// <returns>Toggle GameObject</returns>
    public static GameObject CreateToggle(GameObject parent, ToggleParameters toggleParameters, TransformParameters transformParameters)
        => CreateToggle(parent.transform, toggleParameters, transformParameters);

    /// <summary>
    ///     Create a toggle with parent
    /// </summary>
    /// <param name="parentTransform">Parent GameObject Transform</param>
    /// <param name="toggleParameters">Toggle Parameters</param>
    /// <param name="transformParameters">Transform Parameters</param>
    /// <returns></returns>
    public static GameObject CreateToggle(Transform parentTransform, ToggleParameters toggleParameters, TransformParameters transformParameters)
    {
        var toggle = TglOnGameObject.FastInstantiate(parentTransform);
        toggle.name = toggleParameters.ToggleName;

        var txt = toggle.transform.GetChild(1).gameObject;
        txt.GetComponent<Localization>().Destroy();
        txt.SetTextComponent(toggleParameters.TextParameters);
        txt.AddContentSizeFitter();

        var rectTransform = txt.GetComponent<RectTransform>();
        rectTransform.UpdateTransformLayoutInfo();

        toggle.SetRectTransform(transformParameters);

        toggle.GetComponent<OnToggle>().Destroy();
        toggle.GetComponent<OnToggleOn>().Destroy();
        toggle.GetComponent<OnActivate>().Destroy();

        var toggleComp = toggle.GetComponent<Toggle>();
        toggleComp.onValueChanged.AddListener(toggleParameters.CallBack);
        toggleComp.group = toggleParameters.ToggleGroup;
        toggleComp.SetIsOnWithoutNotify(toggleParameters.InitialValue);

        var checkMark = toggle.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        checkMark.color = toggleParameters.CheckMarkColor;

        GameObjectCache[toggleParameters.ToggleName] = toggle;

        return toggle;
    }

    /// <summary>
    ///     Create a toggle in the PnlMenu
    /// </summary>
    /// <param name="name">GameObject Name</param>
    /// <param name="text">Toggle Text</param>
    /// <param name="initialValue">Initial Value for Toggle</param>
    /// <param name="callback">Boolean Callback</param>
    /// <returns>Toggle GameObject</returns>
    public static GameObject CreatePnlMenuToggle(string name, string text, bool initialValue, Action<bool> callback) =>
        CreatePnlMenuToggle(new ToggleParameters(name, new TextParameters(text, NormalFont, 40), initialValue, callback));

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
        var toggle = TglOnGameObject.FastInstantiate(PnlMenuGameObject.transform);
        toggle.name = name;

        var txt = toggle.transform.GetChild(1).gameObject;
        txt.GetComponent<Localization>().Destroy();
        txt.SetText(text);
        txt.SetColor(ToggleTextColor);
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
        toggleComp.SetIsOnWithoutNotify(expression.Compile()(target));

        toggle.SetParent(PnlOptionGameObject);

        GameObjectCache[name] = toggle;

        return toggle;
    }

    /// <summary>
    ///     Create a toggle in the PnlMenu
    /// </summary>
    /// <param name="toggleParameters">Toggle Parameters</param>
    /// <returns>Toggle GameObject</returns>
    public static GameObject CreatePnlMenuToggle(ToggleParameters toggleParameters)
    {
        var toggle = TglOnGameObject.FastInstantiate(PnlMenuGameObject.transform);
        toggle.name = toggleParameters.ToggleName;

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
        toggleComp.SetIsOnWithoutNotify(toggleParameters.InitialValue);

        var checkMark = toggle.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        checkMark.color = toggleParameters.CheckMarkColor;

        toggle.SetParent(PnlOptionGameObject);

        GameObjectCache[toggleParameters.ToggleName] = toggle;

        return toggle;
    }

    private static Vector3 GetPosition(RectTransform rectTransform)
    {
        var width = rectTransform.rect.width * ScaleFactorX + ToggleSpacing;
        var columnIndex = ToggleCount / 4;

        if (LongestWidths.Count <= columnIndex)
        {
            LongestWidths.Add(0f);
        }

        if (width > LongestWidths[columnIndex])
        {
            LongestWidths[columnIndex] = width;
        }

        var position = Positions[ToggleCount % 4];

        if (columnIndex > 0)
        {
            position.x += LongestWidths.Take(columnIndex).Sum();
        }

        ToggleCount++;
        return position;
    }

    [ExitMainScene]
    private static void Reset(object e, SceneEventArgs args) => ToggleCount = 0;
}