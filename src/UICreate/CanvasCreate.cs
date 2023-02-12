using UnityEngine;
using UnityEngine.UI;

namespace MuseDashMirror.UICreate
{
    public static class CanvasCreate
    {
        /// <summary>
        /// Create a screenspace overlay canvas
        /// </summary>
        public static GameObject CreateCanvas(string canvasName)
        {
            var canvas = new GameObject();
            canvas.name = canvasName;
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            return canvas;
        }

        /// <summary>
        /// Create a screenspace camera canvas with 1920*1080 reference resolution
        /// </summary>
        public static GameObject CreateCanvas(string canvasName, string cameraName)
        {
            var canvas = new GameObject();
            canvas.name = canvasName;
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            canvas.GetComponent<Canvas>().worldCamera = GameObject.Find(cameraName).GetComponent<Camera>();
            canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
            canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvas.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            return canvas;
        }

        /// <summary>
        /// Create a screenspace camera canvas with specified parent using name
        /// </summary>
        public static GameObject CreateCanvas(string canvasName, string cameraName, string parentName)
        {
            var canvas = CreateCanvas(canvasName, cameraName);
            var parent = GameObject.Find(parentName);
            canvas.transform.SetParent(parent.transform);
            return canvas;
        }

        /// <summary>
        /// Create a screenspace camera canvas with specified parent
        /// </summary>
        public static GameObject CreateCanvas(string canvasName, string cameraName, GameObject parent)
        {
            var canvas = CreateCanvas(canvasName, cameraName);
            canvas.transform.SetParent(parent.transform);
            return canvas;
        }
    }
}