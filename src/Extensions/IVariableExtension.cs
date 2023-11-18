using Il2CppAssets.Scripts.PeroTools.Nice.Interface;
using Object = Il2CppSystem.Object;

namespace MuseDashMirror.Extensions;

/// <summary>
///     Extension methods for <see cref="IVariable" />
/// </summary>
public static class IVariableExtension
{
    /// <summary>
    ///     Easy way to use VariableUtils.GetResult
    /// </summary>
    public static T Get<T>(this IVariable variable) => variable.GetResult<T>();

    /// <summary>
    ///     Easy way to use VariableUtils.SetResult
    /// </summary>
    public static void Set(this IVariable variable, Object value) => variable.SetResult(value);
}