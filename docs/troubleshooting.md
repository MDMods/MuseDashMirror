# Troubleshooting

Start with the compiler and MelonLoader log. MuseDashMirror reports invalid generated declarations as compiler or `MDM` diagnostics, while missing Unity objects are generally written to the log by the lookup helpers.

## The project uses the wrong SDK

Run `dotnet --version` in the mod project. It must report a .NET 10 SDK because the bundled source generator depends on its compiler packages. Keep the mod's `TargetFramework` set to `net6.0`; the SDK used to build the project and its runtime target are separate settings.

See [Getting started](getting-started.md#requirements) for the complete project requirements.

## An attributed callback or toggle is not generated

Check these conditions in order:

1. The compilation has exactly one assembly-level `MelonInfo` attribute.
2. The class referenced by `MelonInfo` and each class containing generated members is `partial`.
3. Generated declarations are top-level, non-generic classes in a named namespace.
4. Event handlers are static, return `void`, and use the exact unqualified argument types shown in [Events and scenes](events-and-scenes.md).
5. A `[PnlMenuToggle]` target is a single static `GameObject` field or property, and its bound members are supplied with `nameof`.
6. All compiler errors are fixed, including `CS0260` and `MDM0000` through `MDM0007`.

If a direct subscription works but the equivalent attribute does not, focus on the source-generation requirements rather than the runtime event. See [Source generators](source-generators.md#registration-model) and [Analyzer diagnostics](source-generators.md#analyzer-diagnostics).

## A UI helper cannot find an object

- Create scene-owned UI from the relevant scene or panel callback, after the required camera and hierarchy exist.
- Use `PnlMenuPatch` before calling `CreatePnlMenuToggle`; use `CreateToggle` only after `GameInit.Awake` has cached the game's toggle template.
- Access the bundled fonts only after MuseDashMirror has initialized.
- Give created objects unique, mod-prefixed names because the object and camera caches are name-based.
- Recreate UI after a scene change instead of retaining Unity objects from the previous scene.

See [UI components](ui-components.md) and [GameObject lookup](utilities-and-extensions.md#gameobject-lookup) for the lifecycle and cache details.

## A toggle binding or color behaves unexpectedly

Use the callback overload of `CreatePnlMenuToggle` in the current release. The generic target/expression overload throws while compiling an instance-member setter. To customize the label color, set `TextParameters.Color` directly; changing `ToggleParameters.TextColor` after construction does not update the nested text parameters.

See [Creating option-menu toggles](ui-components.md#creating-option-menu-toggles) for a working example.

## Battle data is empty or changes while reading

Read chart and battle state from a chart lifecycle callback, not during early mod initialization. `MusicDataList` is populated on a background task after `GameStartPatch`, so it may still be incomplete and must not be enumerated concurrently while it is being filled.

`NormalMissCount`, `GhostMissCount`, and `CollectableNoteMissCount` currently have no runtime update path and should not be used as live counters. See [Game data](game-data.md#active-battle-information) for supported values and their availability.
