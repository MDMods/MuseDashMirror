namespace MuseDashMirror.SourceGenerators;

public class LoggerData(string @namespace, string name)
{
    public string Namespace { get; set; } = @namespace;
    public string Name { get; set; } = name;
}