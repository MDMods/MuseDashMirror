namespace MuseDashMirror.Attributes;

/// <summary>
///     Indicate that the method is compatible with the specified scene
/// </summary>
/// <param name="scene">Scene Type</param>
[AttributeUsage(AttributeTargets.Method)]
public sealed class CompatibleSceneAttribute(Scene scene) : Attribute;