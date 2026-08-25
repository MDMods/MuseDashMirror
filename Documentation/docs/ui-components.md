# UI Components

The `MuseDashMirror.UI` namespace contains helpers for creating canvases, text objects, and toggles. The related parameter models live in `MuseDashMirror.Models`.

Create scene-owned UI from a scene or panel callback, after the relevant cameras, fonts, and game objects exist. Do not keep Unity objects from one scene and reuse them in another.

## Canvases and cameras

`CanvasUtils` supports the game's common 2D and 3D cameras as well as arbitrary camera names.

```csharp
using MuseDashMirror.Models;
using MuseDashMirror.UI;

var overlay = CanvasUtils.CreateOverlayCanvas("ExampleMod.Overlay");
var twoDimensional = CanvasUtils.CreateCameraCanvas(
    "ExampleMod.2DCanvas",
    CameraDimension.TwoD);
var threeDimensional = CanvasUtils.CreateCameraCanvas(
    "ExampleMod.3DCanvas",
    CameraDimension.ThreeD);
```

`CameraDimension.TwoD` resolves `Camera_2D`; `CameraDimension.ThreeD` resolves `Camera_3D`. Use `GetCamera(string)` or the camera-name overloads only when a scene has another known camera.

Camera-space canvases are configured with a 1920 x 1080 reference resolution and `ScaleWithScreenSize`. Overloads accepting a parent attach the canvas to that GameObject. `CreateCanvas` is the lower-level method when the render mode and camera must be supplied explicitly.

> [!TIP]
> Prefer `CameraDimension` for standard Muse Dash scenes. Name-based lookup assumes that the requested camera GameObject already exists in the active scene.

## Fonts and colors

MuseDashMirror loads four game fonts during its Melon initialization and releases them during deinitialization:

- `Fonts.NormalFont`
- `Fonts.SnapsTasteFont`
- `Fonts.SourceHanSansCnHeavyFont`
- `Fonts.MiniSimpleSuperThickBlackFont`

`TextParameters` uses `NormalFont`, a font size of 40, white, and middle-center alignment by default. The `Colors` class also exposes `Blue`, `Silver`, `ToggleTextColor`, and `ToggleCheckMarkColor`.

> [!IMPORTANT]
> Use the font properties only after MuseDashMirror has initialized. A field initializer that reads a font too early can capture `null`; construct UI parameters inside a scene or patch callback instead.

## Creating text

`TextGameObjectUtils.CreateText` accepts either a parent GameObject or a parent path, then applies `TextParameters` and `TransformParameters`.

```csharp
using MuseDashMirror.Attributes.EventAttributes.SceneEvents;
using MuseDashMirror.EventArguments;
using MuseDashMirror.Models;
using MuseDashMirror.UI;
using UnityEngine;

namespace ExampleMod;

internal static partial class OverlayUi
{
    [EnterGameScene]
    private static void CreateOverlay(object _, SceneEventArgs args)
    {
        var canvas = CanvasUtils.CreateOverlayCanvas("ExampleMod.Overlay");

        var text = new TextParameters("Ready")
        {
            Color = Colors.Blue,
            FontSize = 48,
            Alignment = TextAnchor.MiddleCenter
        };

        var transform = new TransformParameters(Vector3.zero, isLocalPosition: true);

        TextGameObjectUtils.CreateText(
            "ExampleMod.ReadyText",
            canvas,
            text,
            transform);
    }
}
```

`TextParameters` has overloads for common combinations of text, color, font, size, and alignment. A string also converts implicitly to `TextParameters`.

Use `EllipseTextParameters` when a long middle section should be replaced with `...`. Its range start value is the visible prefix length and its end value is the visible suffix length. If the text is too short for both lengths, it remains unchanged.

## Position and size

`TransformParameters` controls the resulting `RectTransform`:

| Property | Behavior |
| --- | --- |
| `Position` | Target local or global position. |
| `IsLocalPosition` | Uses `localPosition` when true; otherwise applies the canvas scale factor to screen-space coordinates. |
| `LocalScale` | Local scale assigned before layout and position are calculated. |
| `SizeDelta` | Explicit size used by constructors that accept a size. |
| `IsAutoSize` | Adds a `ContentSizeFitter` when true. It becomes false when a `SizeDelta` constructor is used. |
| `PositionStrategy` | Defines whether `Position` identifies the center, left edge, or right edge. |

The default `CenterPositionStrategy` positions the object's center. Use `LeftEdgePositionStrategy` or `RightEdgePositionStrategy` when a coordinate should represent that edge instead:

```csharp
using MuseDashMirror.Models;
using MuseDashMirror.Models.PositionStrategies;
using UnityEngine;

var leftAligned = new TransformParameters(
    new Vector3(100f, 200f, 0f),
    new LeftEdgePositionStrategy());

var fixedSize = new TransformParameters(
    new Vector3(0f, 0f, 0f),
    isLocalPosition: true,
    sizeDelta: new Vector2(400f, 80f));
```

You can implement `IPositionStrategy` for another anchor convention and assign it to `TransformParameters.PositionStrategy`.

## Creating option-menu toggles

Create PnlMenu toggles from `PnlMenuPatch`, after MuseDashMirror has cached the menu and the original toggle used as a template:

```csharp
using MuseDashMirror.Attributes.EventAttributes.PatchEvents;
using MuseDashMirror.EventArguments;
using MuseDashMirror.UI;

namespace ExampleMod;

internal static partial class OptionUi
{
    private static bool IsEnabled { get; set; } = true;

    [PnlMenuPatch]
    private static void CreateToggle(object _, PnlMenuEventArgs args)
    {
        ToggleUtils.CreatePnlMenuToggle(
            "ExampleMod.Enabled",
            "Example Mod",
            IsEnabled,
            value => IsEnabled = value);
    }
}
```

`CreatePnlMenuToggle` clones the game's `TglOn`, removes its localization component and original actions, binds the callback, and places up to four toggles in each column. Use a unique GameObject name so it does not collide with another mod or an internal cache entry.

For custom text, colors, checkmark color, or a `ToggleGroup`, construct `ToggleParameters`:

```csharp
using MuseDashMirror.Models;
using MuseDashMirror.UI;
using UnityEngine;

var parameters = new ToggleParameters(
    "ExampleMod.Enabled",
    new TextParameters("Example Mod", fontSize: 36, alignment: TextAnchor.MiddleLeft),
    IsEnabled,
    value => IsEnabled = value)
{
    CheckMarkColor = Colors.Blue
};

ToggleUtils.CreatePnlMenuToggle(parameters);
```

`ToggleUtils.CreateToggle` provides overloads for a custom parent name, GameObject, or Transform and accepts a separate `TransformParameters`. It still clones the cached game toggle, so call it only after `GameInit.Awake` has initialized that template.

If the label and binding are compile-time constants, `[PnlMenuToggle]` can generate the `PnlMenuPatch` registration and field assignment. See [Source generators](source-generators.md#generated-option-menu-toggle).

## UI caches and scene lifetime

Created text, toggle, and camera-canvas objects are stored in MuseDashMirror's name-based GameObject cache. Camera lookups have a separate name-based cache. Both caches are cleared through scene-exit callbacks.

This has two practical consequences:

- Give created objects stable, mod-prefixed names such as `ExampleMod.StatusText`.
- Recreate scene-owned UI after the next scene loads instead of retaining a reference from the previous scene.

For lower-level lookup, hierarchy traversal, text updates, and layout methods, continue with [Utilities and extensions](utilities-and-extensions.md).
