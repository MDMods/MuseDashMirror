using System.Reflection;
using Assets.Scripts.PeroTools.Nice.Interface;
using Il2CppSystem;
using static MuseDashMirror.Main;
using Type = System.Type;

namespace MuseDashMirror;

/// <summary>
/// Extension methods and patch methods
/// </summary>
public static class Utils
{
    /// <summary>
    /// Easy way to use VariableUtils.GetResult
    /// </summary>
    public static T Get<T>(this IVariable variable) => VariableUtils.GetResult<T>(variable);

    /// <summary>
    /// Easy way to use VariableUtils.SetResult
    /// </summary>
    public static void Set(this IVariable variable, Object value) => VariableUtils.SetResult(variable, value);

    /// <summary>
    /// Prefix patch corresponding method with method info
    /// </summary>
    public static void PrefixPatch(MethodInfo targetMethodInfo, MethodInfo prefixMethodInfo)
        => harmony.Patch(targetMethodInfo, new HarmonyMethod(prefixMethodInfo));

    /// <summary>
    /// Prefix patch corresponding method with class and method name
    /// </summary>
    public static void PrefixPatch(Type targetClass, string targetMethodName, Type prefixOwnClass, string prefixMethodName)
        => harmony.Patch(targetClass.GetMethod(targetMethodName), new HarmonyMethod(prefixOwnClass.GetMethod(prefixMethodName)));

    /// <summary>
    /// Postfix patch corresponding method with method info
    /// </summary>
    public static void PostfixPatch(MethodInfo targetMethodInfo, MethodInfo postfixMethodInfo)
        => harmony.Patch(targetMethodInfo, postfix: new HarmonyMethod(postfixMethodInfo));

    /// <summary>
    /// Postfix patch corresponding method with class and method name
    /// </summary>
    public static void PostfixPatch(Type targetClass, string targetMethodName, Type postfixOwnClass, string postfixMethodName)
        => harmony.Patch(targetClass.GetMethod(targetMethodName), postfix: new HarmonyMethod(postfixOwnClass.GetMethod(postfixMethodName)));

    /// <summary>
    /// Prefix and Postfix patch corresponding method with method info
    /// </summary>
    public static void PatchBoth(MethodInfo targetMethodInfo, MethodInfo prefixMethodInfo, MethodInfo postfixMethodInfo)
        => harmony.Patch(targetMethodInfo, new HarmonyMethod(prefixMethodInfo), new HarmonyMethod(postfixMethodInfo));

    /// <summary>
    /// Prefix and Postfix patch corresponding method with class and method name
    /// </summary>
    public static void PatchBoth(Type targetClass, string targetMethodName, Type prefixOwnClass, string prefixMethodName, Type postfixOwnClass, string postfixMethodName)
        => harmony.Patch(targetClass.GetMethod(targetMethodName), new HarmonyMethod(prefixOwnClass.GetMethod(prefixMethodName)), new HarmonyMethod(postfixOwnClass.GetMethod(postfixMethodName)));
}