# Introduction

MuseDashMirror collects common Muse Dash modding tasks behind a small runtime library and a bundled Roslyn analyzer/source-generator package.

## What it provides

| Area | Main APIs | Typical use |
| --- | --- | --- |
| Scenes | `SceneInfo` and scene attributes | Run code when any scene, the main menu, a chart, loading, or welcome scene is entered or exited. |
| Game callbacks | `PatchEvents` and patch attributes | React after menu, chart-start, score, language, and other supported game methods run. |
| Game data | `PlayerData` and `BattleComponent` | Read player selections and current-chart state, or restart and exit a chart. |
| UI | `CanvasUtils`, `TextGameObjectUtils`, and `ToggleUtils` | Create canvases, labels, and option-menu toggles using the game's fonts and controls. |
| Utilities | `GameObjectUtils` and extension methods | Find inactive descendants, traverse transforms, update text/layout, and work with common collections. |
| Code generation | `[Logger]`, event attributes, and `[PnlMenuToggle]` | Generate registration and boilerplate at compile time. |

## Runtime and compile-time parts

The `MuseDashMirror` assembly is loaded as a MelonLoader mod and owns the Harmony patches, scene state, fonts, caches, and helper methods. Your mod references this runtime assembly.

The NuGet package also contains `MuseDashMirror.CodeAnalysis` as an analyzer. It validates attributed declarations and generates the code that connects them to the runtime events. No reflection scan is required to discover those declarations at startup.

> [!IMPORTANT]
> Generated registration depends on the standard assembly-level `MelonInfo` attribute identifying exactly one `MelonMod` class. That class and every class containing generated members must be declared `partial`.

## Choose a guide

- New project: [Getting started](getting-started.md)
- Lifecycle and callbacks: [Events and scenes](events-and-scenes.md)
- Generated declarations and diagnostics: [Source generators](source-generators.md)
- Player and chart state: [Game data](game-data.md)
- UI construction: [UI components](ui-components.md)
- GameObject and extension helpers: [Utilities and extensions](utilities-and-extensions.md)
- Build and runtime problems: [Troubleshooting](troubleshooting.md)

The guides focus on correct lifecycle and common usage patterns.
