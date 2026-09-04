# Source Generators

The NuGet package includes the `MuseDashMirror.CodeAnalysis` analyzer assembly. It generates loggers, event subscriptions, option-menu toggles, and a single registration entry point at compile time.

## Registration model

The generator finds the assembly-level `MelonInfo` attribute and uses its first argument as the mod entry class. It then creates a static constructor on that class that calls every generated registration method.

For registration to work:

1. The compilation must contain exactly one `MelonInfo` attribute.
2. The class passed to `MelonInfo` must inherit `MelonMod` and be `partial`.
3. A class containing an event handler, generated logger, or generated toggle must be `partial`.
4. Generated event handlers and toggle targets must be static.
5. Generated declarations must be top-level, non-generic classes in a named namespace.
6. The mod entry class must not declare another static constructor when generated registrations are present.

You do not call the generated `Register...` methods yourself.

Missing `partial` modifiers are reported by the C# compiler as `CS0260`; they are not separate `MDM` diagnostics.

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

`[PnlMenuToggle]` creates a toggle whenever `PnlMenu.Awake` runs, assigns the resulting `GameObject` to the attributed member, and binds it to a static Boolean field or property identified by its declaring type and member name.

```csharp
using MuseDashMirror.Attributes;
using UnityEngine;

namespace ExampleMod;

internal static partial class Options
{
    private static bool IsEnabled { get; set; } = true;

    [PnlMenuToggle("ExampleMod.Enabled", "Example Mod", nameof(Options.IsEnabled))]
    private static GameObject EnabledToggle { get; set; } = null!;
}
```

The three constructor arguments are:

1. The created GameObject name.
2. The visible label text.
3. The bound static Boolean field or property, supplied with `nameof`.

An optional fourth argument identifies a static `ToggleGroup` field or property, also supplied with `nameof`. Toggles that pass the same group participate in that group:

```csharp
[PnlMenuToggle(
    "ExampleMod.ModeA",
    "Mode A",
    nameof(Settings.IsModeA),
    nameof(Options.ModeToggleGroup))]
private static GameObject ModeAToggle { get; set; } = null!;
```

Although `nameof` evaluates to an unqualified string, the source generator resolves its operand with Roslyn and emits the fully qualified declaring type and member name. The Boolean and group members must be static and accessible from the attributed class, and the `ToggleGroup` must exist when `PnlMenu.Awake` runs.

The attributed field or property must be a single, static `GameObject` declaration in a partial class.

> [!NOTE]
> The label is emitted as a string literal; `[PnlMenuToggle]` does not automatically localize it. Use `ToggleUtils` directly when the label must be selected dynamically.

## Analyzer diagnostics

The bundled analyzers report invalid declarations as errors before broken generated code reaches runtime.

| Rule | Meaning | Fix |
| --- | --- | --- |
| `MDM0000` | A patch callback has the wrong parameters. | Use `object` and the matching patch `EventArgs` type. |
| `MDM0001` | A scene callback has the wrong parameters. | Use `object` and `SceneEventArgs`. |
| `MDM0002` | An attributed callback does not return `void`. | Change its return type to `void`. |
| `MDM0003` | A generated callback that must be static is not static. | Add the `static` modifier. |
| `MDM0004` | A generated toggle member is not a `GameObject`. | Change the field or property type to `GameObject`. |
| `MDM0005` | A generated toggle member is not static. | Add the `static` modifier. |
| `MDM0006` | `[PnlMenuToggle]` is used on a declaration containing multiple fields. | Put the attributed field in its own declaration. |
| `MDM0007` | A Boolean or `ToggleGroup` member argument does not use `nameof` to reference a field or property. | Replace the string or other expression with `nameof(Type.Member)`. |

If an attributed declaration compiles but does not run, first verify that the assembly contains one `MelonInfo` and that the generated member is in the same compilation as that mod class.
