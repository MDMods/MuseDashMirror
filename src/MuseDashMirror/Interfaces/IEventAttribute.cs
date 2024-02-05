using System.Reflection;

namespace MuseDashMirror.Interfaces;

/// <summary>
///     Interface for event attributes
/// </summary>
public interface IEventAttribute
{
    /// <summary>
    ///     Register the method to the event
    /// </summary>
    /// <param name="method"></param>
    void Register(MethodInfo method);
}