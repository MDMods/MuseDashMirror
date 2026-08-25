---
_disableToc: true
---

# MuseDashMirror

MuseDashMirror is a library for Muse Dash mods built with MelonLoader. It provides scene and game callbacks, commonly used game data, UI helpers, Unity extensions, and source generators that wire attributed members without runtime reflection.

## Start here

- [Getting started](docs/getting-started.md) covers project setup and a first scene callback.
- [Events and scenes](docs/events-and-scenes.md) lists the available lifecycle and game patch hooks.
- [Source generators](docs/source-generators.md) explains generated loggers, callbacks, and option-menu toggles.
- [Game data](docs/game-data.md) describes `PlayerData` and `BattleComponent` and when their values are available.
- [UI components](docs/ui-components.md) covers canvases, text, toggles, fonts, layout, and positioning.
- [Utilities and extensions](docs/utilities-and-extensions.md) covers GameObject lookup and the general extension methods.

For exact signatures and overloads, see the [API documentation](xref:MuseDashMirror).

## Installation

The recommended starting point is [MuseDash.Mod.Template](https://github.com/lxymahatma/MuseDash.Mod.Template), with `MuseDashMirror` selected as a useful library. For an existing MelonLoader mod project, install the NuGet package:

```powershell
dotnet add package MuseDashMirror
```

> [!NOTE]
> MuseDashMirror does not replace MelonLoader or the Muse Dash IL2CPP reference assemblies required by a mod project. The template is the easiest way to configure those references.
