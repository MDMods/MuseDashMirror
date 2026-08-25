# Source Generators

The NuGet package includes the `MuseDashMirror.CodeAnalysis` analyzer assembly. It generates loggers, event subscriptions, option-menu toggles, and a single registration entry point at compile time.

## Registration model

The generator finds the assembly-level `MelonInfo` attribute and uses its first argument as the mod entry class. It then creates a static constructor on that class that calls every generated registration method.

For registration to work:

1. The compilation must contain exactly one `MelonInfo` attribute.
2. The class passed to `MelonInfo` must inherit `MelonMod` and be `partial`.
3. A class containing an event handler, generated logger, or generated toggle must be `partial`.
4. Generated event handlers and toggle targets must be static.

You do not call the generated `Register...` methods yourself.

## Generated logger

Apply `[Logger]` to a partial class to generate a private `MelonLogger.Instance` field named `Logger` using the class name as its log source:

```csharp
using MuseDashMirror.Attributes;

namespace ExampleMod;

[Logger]
internal static partial class ChartReader
{
    public static void Read()
    {
        Logger.Msg("Reading chart data");
    }
}
```

The default `LoggerType.StaticReadonly` produces a `private static readonly` field. Use `LoggerType.Readonly` only for a non-static partial class that needs an instance logger:

```csharp
using MuseDashMirror.Attributes;
using MuseDashMirror.Shared;

[Logger(LoggerType.Readonly)]
internal partial class Worker
{
    public void Run() => Logger.Msg("Running");
}
```

## Generated event subscriptions

Scene and patch-event attributes generate subscriptions to `SceneInfo` and `PatchEvents`. The handler class must be partial and the handler must be static, return `void`, and use the exact two-parameter signature.

```csharp
using MuseDashMirror.Attributes.EventAttributes.SceneEvents;
using MuseDashMirror.EventArguments;

namespace ExampleMod;

internal static partial class Hooks
{
    [EnterMainScene]
    [ExitMainScene]
    private static void OnMainSceneChanged(object _, SceneEventArgs args)
    {
    }
}
```

A scene method may have more than one compatible scene attribute. See [Events and scenes](events-and-scenes.md) for the full attribute list and patch argument types.

## Generated option-menu toggle

`[PnlMenuToggle]` creates a toggle whenever `PnlMenu.Awake` runs, assigns the resulting `GameObject` to the attributed member, and binds it to a Boolean expression supplied as text.

```csharp
using MuseDashMirror.Attributes;
using UnityEngine;

namespace ExampleMod;

internal static partial class Options
{
    private static bool IsEnabled { get; set; } = true;

    [PnlMenuToggle("ExampleMod.Enabled", "Example Mod", "IsEnabled")]
    private static GameObject EnabledToggle { get; set; } = null!;
}
```

The three constructor arguments are:

1. The created GameObject name.
2. The visible label text.
3. A Boolean field or property expression inserted into generated code, such as `IsEnabled` or `Settings.IsEnabled`.

The attributed field or property must be a single, static `GameObject` declaration in a partial class. The generated field assignment and Boolean expression must be accessible from that class.

> [!NOTE]
> The label is emitted as a string literal; `[PnlMenuToggle]` does not automatically localize it. Use `ToggleUtils` directly when the label must be selected dynamically.

## Analyzer diagnostics

The bundled analyzers report invalid declarations as errors before broken generated code reaches runtime.

| Rule | Meaning | Fix |
| --- | --- | --- |
| `MDM0000` | `[Logger]` is on a non-partial class. | Add the `partial` modifier. |
| `MDM0001` | The `MelonMod` entry class is not partial. | Make the class referenced by `MelonInfo` partial. |
| `MDM0100` | A patch callback has the wrong parameters. | Use `object` and the matching patch `EventArgs` type. |
| `MDM0101` | A scene callback has the wrong parameters. | Use `object` and `SceneEventArgs`. |
| `MDM0102` | An attributed callback does not return `void`. | Change its return type to `void`. |
| `MDM0103` | A generated callback that must be static is not static. | Add the `static` modifier. |
| `MDM0104` | An attributed callback is in a non-partial class. | Add the `partial` modifier to its class. |
| `MDM0200` | A generated toggle member is in a non-partial class. | Add the `partial` modifier. |
| `MDM0201` | A generated toggle member is not a `GameObject`. | Change the field or property type to `GameObject`. |
| `MDM0202` | A generated toggle member is not static. | Add the `static` modifier. |
| `MDM0203` | `[PnlMenuToggle]` is used on a declaration containing multiple fields. | Put the attributed field in its own declaration. |

If an attributed declaration compiles but does not run, first verify that the assembly contains one `MelonInfo`, its mod class is partial, and the generated member is in the same compilation as that mod class.
