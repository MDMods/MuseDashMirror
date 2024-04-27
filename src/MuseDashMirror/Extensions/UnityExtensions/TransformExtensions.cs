namespace MuseDashMirror.Extensions.UnityExtensions;

/// <summary>
///     <see cref="Transform" /> Extension Methods
/// </summary>
public static class TransformExtensions
{
    /// <summary>
    ///     Get the Child Transform of the <paramref name="transform" /> at the specified <paramref name="indexes" />
    /// </summary>
    /// <param name="transform">Transform</param>
    /// <param name="indexes">Indexes</param>
    /// <returns>Child Transform</returns>
    public static Transform GetChildTransform(this Transform transform, params int[] indexes)
        => indexes.Aggregate(transform, (current, index) => current.GetChild(index));

    /// <summary>
    ///     Get the Child GameObject of the <paramref name="transform" /> at the specified <paramref name="indexes" />
    /// </summary>
    /// <param name="transform">Transform</param>
    /// <param name="indexes">Indexes</param>
    /// <returns>Child GameObject</returns>
    public static GameObject GetChildGameObject(this Transform transform, params int[] indexes)
        => GetChildTransform(transform, indexes).gameObject;

    /// <summary>
    ///     Destroy all children GameObject of a Transform
    /// </summary>
    /// <param name="transform">Transform</param>
    public static void DestroyAllChildren(this Transform transform)
    {
        foreach (var child in transform)
        {
            child.Cast<Transform>().gameObject.Destroy();
        }
    }

    /// <summary>
    ///     Destroy all children GameObject of a Transform immediately
    /// </summary>
    /// <param name="transform">Transform</param>
    public static void DestroyAllChildrenImmediate(this Transform transform)
    {
        foreach (var child in transform)
        {
            child.Cast<Transform>().gameObject.DestroyImmediate();
        }
    }

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