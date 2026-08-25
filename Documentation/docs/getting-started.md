# Getting Started

## Requirements

MuseDashMirror is intended for a Muse Dash IL2CPP mod project using MelonLoader and targeting .NET 6. The project must already reference the MelonLoader, Unity, and generated Muse Dash assemblies it uses.

The recommended setup is [MuseDash.Mod.Template](https://github.com/lxymahatma/MuseDash.Mod.Template). Select `MuseDashMirror` in its `UsefulLibs` option and let the template configure the game references and build layout.

For an existing project, add the package directly:

```powershell
dotnet add package MuseDashMirror
```

## Prepare the mod entry point

Keep the normal assembly-level `MelonInfo` declaration and make the referenced mod class `partial`. MuseDashMirror uses that declaration as the generated registration entry point.

```csharp
using MelonLoader;

[assembly: MelonInfo(typeof(ExampleMod.ExampleMod), "Example Mod", "1.0.0", "Author")]

namespace ExampleMod;

public sealed partial class ExampleMod : MelonMod
{
}
```

There must be exactly one `MelonInfo` declaration in the compilation. If no unique mod class can be found, generated event and toggle registrations are not emitted.

## Add a first callback

Create a `partial` class and add a static method with a scene attribute:

```csharp
using MelonLoader;
using MuseDashMirror.Attributes.EventAttributes.SceneEvents;
using MuseDashMirror.EventArguments;

namespace ExampleMod;

internal static partial class SceneHooks
{
    [EnterMainScene]
    private static void OnEnterMainScene(object _, SceneEventArgs args)
    {
        MelonLogger.Msg($"Entered {args.SceneName} ({args.BuildIndex})");
    }
}
```

The source generator creates the event subscription and connects it through the mod entry point. You do not need to call a registration method yourself.

## Direct subscription is also available

Attributes are optional. You can subscribe to the same public events manually when explicit ownership and unsubscription are preferable:

```csharp
using MelonLoader;
using MuseDashMirror;
using MuseDashMirror.EventArguments;

namespace ExampleMod;

public sealed partial class ExampleMod : MelonMod
{
    public override void OnInitializeMelon() =>
        SceneInfo.OnEnterMainScene += OnEnterMainScene;

    public override void OnDeinitializeMelon() =>
        SceneInfo.OnEnterMainScene -= OnEnterMainScene;

    private static void OnEnterMainScene(object? _, SceneEventArgs args) =>
        MelonLogger.Msg($"Entered {args.SceneName}");
}
```

Continue with [Events and scenes](events-and-scenes.md) for every supported callback, or [UI components](ui-components.md) to create a first on-screen element.
