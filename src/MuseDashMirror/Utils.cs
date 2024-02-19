using System.Reflection;

namespace MuseDashMirror;

/// <summary>
///     Extension methods and patch methods
/// </summary>
[Logger]
public static partial class Utils
{
    /// <summary>
    ///     Get types from assembly
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetTypesFromAssembly(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            Logger.Msg($"Error when getting types from assembly {assembly}:\r\n{ex}");
            return ex.Types.Where(type => type is not null).ToArray()!;
        }
    }
}