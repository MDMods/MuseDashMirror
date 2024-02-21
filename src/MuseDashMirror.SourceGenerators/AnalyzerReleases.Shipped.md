## Release 1.0

### New Rules

| Rule ID | Category                                                                    | Severity | Notes                                                                                    |
|---------|-----------------------------------------------------------------------------|----------|------------------------------------------------------------------------------------------|
| MDM0000 | MuseDashMirror.SourceGenerators.Analyzers.LoggerAnalyzer                    | Error    | Logger attribute can only be used on partial class.                                      |
| MDM0100 | MuseDashMirror.SourceGenerators.Analyzers.EventAnalyzers.PatchEventAnalyzer | Error    | Method with PatchEvent attribute must have parameter types object and EventArgs.         |
| MDM0101 | MuseDashMirror.SourceGenerators.Analyzers.EventAnalyzers.SceneEventAnalyzer | Error    | Method with SceneEvent attribute must have parameter types object and SceneEventArgs.    |
| MDM0102 | MuseDashMirror.SourceGenerators.Analyzers.EventAnalyzers.EventAnalyzer      | Error    | Method with Event attribute must have return type void.                                  |
| MDM0103 | MuseDashMirror.SourceGenerators.Analyzers.EventAnalyzers.EventAnalyzer      | Error    | Method with static class or register type static constructor must be declared as static. |