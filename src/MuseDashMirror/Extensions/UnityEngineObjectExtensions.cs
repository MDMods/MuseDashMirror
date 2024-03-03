using Object = UnityEngine.Object;

namespace MuseDashMirror.Extensions;

/// <summary>
///     <see cref="UnityEngine.Object" /> Extension Methods
/// </summary>
public static class UnityEngineObjectExtensions
{
    /// <summary>
    ///     Destroy a <see cref="UnityEngine.Object" /> Component
    /// </summary>
    /// <param name="obj">Object</param>
    public static void Destroy(this Object obj) => Object.Destroy(obj);
}