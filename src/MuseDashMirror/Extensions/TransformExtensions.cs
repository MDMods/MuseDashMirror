namespace MuseDashMirror.Extensions;

/// <summary>
///     Extension methods for <see cref="Transform" />
/// </summary>
public static class TransformExtensions
{
    /// <summary>
    ///     Get the child of the <paramref name="transform" /> at the specified <paramref name="indexes" />
    /// </summary>
    /// <param name="transform">Transform</param>
    /// <param name="indexes">Indexes</param>
    /// <returns>Transform</returns>
    public static Transform GetChild(this Transform transform, params int[] indexes)
        => indexes.Aggregate(transform, (current, index) => current.GetChild(index));
}