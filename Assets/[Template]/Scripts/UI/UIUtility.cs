using UnityEngine;

namespace Template
{
    public static class UIUtility
    {

        public static Vector2 WorldToUI(Transform worldPoint, Canvas canvas, RectTransform canvasRT, Camera camera)
        {
            return WorldToUI(worldPoint.position, canvas, canvasRT, camera);
        }
        public static Vector2 WorldToUI(Vector3 worldPosition, Canvas canvas, RectTransform canvasRT, Camera camera)
        {
            // Convert the world position to screen space
            Vector3 screenPosition = camera.WorldToScreenPoint(worldPosition);

            // Convert the screen position to local position in the canvas
            Vector2 localPosition;
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRT, 
                screenPosition, 
                canvas.worldCamera,
                out localPosition);

            return localPosition;
        }

        public static Vector2 GetRootCanvasPosition(RectTransform rt, RectTransform canvasRT)
        {
            Vector2 worldPosition = rt.position;
            Vector2 localPosition = canvasRT.InverseTransformPoint(worldPosition);

            return localPosition;
        }
    }
}