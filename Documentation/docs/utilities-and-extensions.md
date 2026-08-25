# Utilities and Extensions

MuseDashMirror includes helpers for locating Unity objects, traversing hierarchy paths, configuring common UI components, and handling a few recurring collection and string operations.

## GameObject lookup

`GameObjectUtils.GetGameObjectWithPath` starts with `GameObject.Find` for the first path segment and then follows descendants with `Transform.Find`:

```csharp
using static MuseDashMirror.Utils.GameObjectUtils;

var label = GetGameObjectWithPath("RootCanvas/Panel/Title");
var cachedLabel = GetGameObjectWithPath(
    "RootCanvas/Panel/Title",
    cacheTargetGameObject: true,
    cacheNodeGameObjects: true);
```

The active root must be discoverable by `GameObject.Find`; descendants may be inactive. A missing segment is logged and the method returns `null`.

> [!IMPORTANT]
> The internal cache is keyed by individual GameObject names, not full hierarchy paths. Avoid opting into caching when different branches contain objects with the same name. The cache is cleared on every scene exit, so never treat a cached scene object as valid across scenes.

## Unity hierarchy and component extensions

Import `MuseDashMirror.Extensions.UnityExtensions` for the Unity-specific methods.

| Target | Useful methods |
| --- | --- |
| `Transform` | `GetChild(2, 3, 0)` follows several child indexes; `GetAncestorAtLevel` walks toward the root. |
| `GameObject` | `GetParentGameObject`, `GetParentTransform`, and `SetParent` simplify hierarchy operations. |
| `GameObject` | `FindComponentInAncestors` and `TryFindComponentInAncestors` search the object and its parents. |
| `GameObject` | `GetTotalScaleFactor` combines local scales; `GetCanvasScalerFactor` reads the nearest `CanvasScaler`. |
| `GameObject` | `SetText`, `SetColor`, `SetTextComponent`, `SetRectTransform`, and `AddContentSizeFitter` configure common UI state. |
| `Component` | `SetText` updates a `Text` component on the same object. |
| `RectTransform` | `UpdateTransformLayoutInfo` forces an immediate layout rebuild. |
| `UnityEngine.Object` | `Destroy` forwards to Unity's object destruction API. |

Example hierarchy traversal:

```csharp
using MuseDashMirror.Extensions.UnityExtensions;

var icon = panel.transform.GetChild(2, 3, 0).gameObject;
var canvas = icon.FindComponentInAncestors<Canvas>();

if (icon.TryFindComponentInAncestors(out CanvasScaler scaler))
{
    var referenceWidth = scaler.referenceResolution.x;
}
```

The non-`Try` ancestor lookup returns `null` when no matching component exists. `GetAncestorAtLevel` returns the highest ancestor it can reach if the requested level is above the root, and returns the original transform for values below one.

## Text shortening

`GetVisibleTextWithEllipsisOrDefault` keeps a prefix and suffix and replaces the middle with `...`. The `Range` start value is treated as the prefix length and its end value as the suffix length:

```csharp
using MuseDashMirror.Extensions;

var shortened = "A very long chart title".GetVisibleTextWithEllipsisOrDefault(new Range(8, 5));
// "A very l...title"
```

If the source string is shorter than the two requested visible lengths combined, the original string is returned. `EllipseTextParameters` applies the same operation before creating a text component.

## Collection helpers

Import `MuseDashMirror.Extensions.CollectionExtensions` for these helpers:

- `Execute` invokes an action for each element and safely does nothing when the sequence is `null`.
- `GetFieldInfosFromTypesByAttribute`, `GetPropertyInfosFromTypesByAttribute`, `GetMethodInfosFromTypesByAttribute`, and `GetMemberInfosFromTypesByAttribute` filter reflected members from a sequence of `Type` values.
- Each reflection helper has a generic attribute overload and a `Type` overload, with optional `BindingFlags`.

These reflection helpers are general utilities. MuseDashMirror's own attributed callback registration uses source generation and does not require them.

See [UI components](ui-components.md) for higher-level canvas, text, toggle, and positioning APIs.
