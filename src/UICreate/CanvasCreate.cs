using UnityEngine;
using UnityEngine.UI;

namespace MuseDashMirror.UICreate
{
    public static class CanvasCreate
    {
        // 去掉最后，parent设置两个重载，名字和gameobject

        /// <summary>
        /// Create a screenspace overlay canvas
        /// </summary>

        public static void CreateCanvas(string canvasName)
        {
            var canvas = new GameObject();
            canvas.name = canvasName;
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
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
        /// Create a screenspace camera canvas with specified reference resolution
        /// </summary>
        public static void CreateCanvas(string canvasName, string cameraName, Vector2 referenceResolution)
        {
            var canvas = CreateCanvas(canvasName, cameraName);
            canvas.GetComponent<CanvasScaler>().referenceResolution = referenceResolution;
        }
    }
}