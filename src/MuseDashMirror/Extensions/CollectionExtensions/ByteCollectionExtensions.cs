namespace MuseDashMirror.Extensions.CollectionExtensions;

/// <summary>
///     Collection Extension Methods for <see cref="byte" />
/// </summary>
public static class ByteCollectionExtensions
{
    /// <summary>
    ///     Convert <see cref="byte" /> array to <see cref="Sprite" />
    /// </summary>
    /// <param name="bytes">Byte Array</param>
    /// <returns>Sprite</returns>
    public static Sprite ToSprite(this byte[] bytes)
    {
        var tex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        tex.LoadImage(bytes);

        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    }
}