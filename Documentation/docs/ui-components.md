# Using UI Components

**Muse Dash Mirror provides many useful classes in the namespace `MuseDashMirror.UIComponents` to help you create UI components.**

## Canvas Utils

**`CanvasUtils` is a static class that provides useful methods to help you create Canvas.**

* `GetCamera` method, which has 2 overloads:
  * `Camera GetCamera(CameraDimension dimension)`: Get the camera using the provided dimension.
  * `Camera GetCamera(string cameraName)`: Get the camera using the provided name.

> [!TIP]
> It is recommended to use the `CameraDimension` enum to get the camera because Muse Dash only uses 2D and 3D cameras in most scenes,
> Therefore, the provided `CameraDimension` enum is enough for most use cases.

* `CreateOverlayCanvas` method: `GameObject CreateOverlayCanvas(string canvasName)`: Create a new `ScreenSpaceOverlay` canvas with the provided canvas name.
* `CreateCameraCanvas` method, which has 4 overloads:
  * `GameObject CreateCameraCanvas(string canvasName, string cameraName)`: Create a new `ScreenSpaceCamera` canvas with the provided canvas name and camera.
  * `GameObject CreateCameraCanvas(string canvasName, CameraDimension dimension)`: Create a new `ScreenSpaceCamera` canvas with the provided canvas name and camera dimension.
  * `GameObject CreateCameraCanvas(string canvasName, string cameraName, GameObject parent)`: Create a new `ScreenSpaceCamera` canvas with the provided name, camera, and parent.
  * `GameObject CreateCameraCanvas(string canvasName, CameraDimension cameraDimension, GameObject parent)`: Create a new `ScreenSpaceCamera` canvas with the provided name, camera dimension, and parent.
* `CreateCanvas` method, which is the base of all the other methods: `GameObject CreateCanvas(string canvasName, RenderMode renderMode, Camera camera, GameObject parent)`

> [!NOTE]
> Even though the `GetCamera` methods are public and available in your code, you should not call it yourself unless you need it.