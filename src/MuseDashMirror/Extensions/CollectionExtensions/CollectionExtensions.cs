namespace MuseDashMirror.Extensions.CollectionExtensions;

/// <summary>
///     Collection Extension Methods for any Type
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    ///     Execute an action for each element in the sequence
    /// </summary>
    /// <param name="sequence"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    public static void Execute<T>(this IEnumerable<T> sequence, Action<T> action)
    {
        if (sequence is null)
        {
            return;
        }

        using var enumerator = sequence.GetEnumerator();
        while (enumerator.MoveNext())
        {
            action(enumerator.Current);
        }
    }
}