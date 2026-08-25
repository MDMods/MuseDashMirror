namespace MuseDashMirror.Extensions.UnityExtensions;

/// <summary>
///     <see cref="Transform" /> Extension Methods
/// </summary>
public static class TransformExtensions
{
    /// <param name="transform">Transform</param>
    extension(Transform transform)
    {
        /// <summary>
        ///     Get the child of the <paramref name="transform" /> at the specified <paramref name="indexes" />
        /// </summary>
        /// <param name="indexes">Indexes</param>
        /// <returns>Transform</returns>
        public Transform GetChild(params ReadOnlySpan<int> indexes)
        {
            foreach (var index in indexes)
            {
                transform = transform.GetChild(index);
            }

            return transform;
        }

        /// <summary>
        ///     Get the Ancestor Transform of a Transform with a specified number of levels up.
        /// </summary>
        /// <param name="ancestorLevels">The number of levels up to look for the ancestor<br />Must be greater than or equal to 1</param>
        /// <returns>Ancestor Transform</returns>
        public Transform GetAncestorAtLevel(int ancestorLevels = 1)
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
}
