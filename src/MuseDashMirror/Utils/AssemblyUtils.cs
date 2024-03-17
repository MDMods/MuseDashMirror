using System.Reflection;

namespace MuseDashMirror.Utils;

/// <summary>
///     Methods related to <see cref="Assembly" />
/// </summary>
[Logger]
public static partial class AssemblyUtils
{
    /// <summary>
    ///     Read embedded resource from the assembly
    /// </summary>
    /// <param name="resourcePath">Resource Path, '.' as the path separator</param>
    /// <returns>Byte Array</returns>
    public static byte[] ReadEmbeddedResource(string resourcePath)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{resourcePath}");

        if (stream is null)
        {
            Logger.Error($"Resource not found: {resourcePath}");
            return Enumerable.Empty<byte>().ToArray();
        }

        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);

        return memoryStream.ToArray();
    }
}