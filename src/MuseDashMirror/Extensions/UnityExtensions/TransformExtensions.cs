namespace MuseDashMirror.Extensions.UnityExtensions;

/// <summary>
///     <see cref="Transform" /> Extension Methods
/// </summary>
public static class TransformExtensions
{
    /// <summary>
    ///     Get the child of the <paramref name="transform" /> at the specified <paramref name="indexes" />
    /// </summary>
    /// <param name="transform">Transform</param>
    /// <param name="indexes">Indexes</param>
    /// <returns>Child Transform</returns>
    public static Transform GetChild(this Transform transform, params int[] indexes)
        => indexes.Aggregate(transform, (current, index) => current.GetChild(index));

    /// <summary>
    ///     Get the Ancestor Transform of a Transform with a specified number of levels up.
    /// </summary>
    /// <param name="transform">Transform</param>
    /// <param name="ancestorLevels">The number of levels up to look for the ancestor<br />Must be greater than or equal to 1</param>
    /// <returns>Ancestor Transform</returns>
    public static Transform GetAncestorAtLevel(this Transform transform, int ancestorLevels = 1)
    {
        if (ancestorLevels < 1)
        {
            return transform;
        }

        while (ancestorLevels-- > 0 && transform.parent != null)
        {
            transform = transform.parent;
        }

        return transform;
    }
}