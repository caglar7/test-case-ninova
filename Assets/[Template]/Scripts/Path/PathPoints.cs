using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public class PathPoints : MonoBehaviour
    {
        public LineRenderer lineRenderer; // Reference to the LineRenderer component
        public Transform[] controlPoints; // An array of control points
        private List<Vector3> points;

        public void Init()
        {
            points = new List<Vector3>();
            
            for (int i = 0; i < controlPoints.Length; i++)
            {
                points.Add(controlPoints[i].position);
            }

            CreatePath(points.ToArray(), 100);
        }

        /// <summary>
        /// Create a path with points evenly distributed along the path
        /// </summary>
        /// <param name="points"></param>
        /// <param name="numberOfPoints"></param>
        private void CreatePath(Vector3[] points, int numberOfPoints)
        {
            if (points.Length < 2)
            {
                Debug.LogError("At least 2 control points are required.");
                return;
            }

            float pathLength = 0f;
            Vector3[] path = new Vector3[numberOfPoints];

            for (int i = 1; i < points.Length; i++)
            {
                pathLength += Vector3.Distance(points[i - 1], points[i]);
            }

            float segmentLength = pathLength / (numberOfPoints - 1);
            float currentDistance = 0f;
            int pathIndex = 0;

            for (int i = 1; i < points.Length; i++)
            {
                float segmentDistance = Vector3.Distance(points[i - 1], points[i]);

                while (currentDistance + segmentDistance >= pathIndex * segmentLength)
                {
                    float t = (pathIndex * segmentLength - currentDistance) / segmentDistance;
                    path[pathIndex] = Vector3.Lerp(points[i - 1], points[i], t);
                    pathIndex++;

                    if (pathIndex >= numberOfPoints)
                    {
                        break;
                    }
                }

                currentDistance += segmentDistance;
            }

            path[numberOfPoints - 1] = points[points.Length - 1];

            lineRenderer.positionCount = numberOfPoints;
            lineRenderer.SetPositions(path);
        }

        /// <summary>
        /// Get a point on the path based on a normalized parameter (0 to 1)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Vector3 GetPointOnPath(float t)
        {
            if (lineRenderer.positionCount < 2)
            {
                Debug.LogError("At least 2 points are required on the path.");
                return Vector3.zero;
            }

            t = Mathf.Clamp01(t); 

            int index = Mathf.FloorToInt(t * (lineRenderer.positionCount - 1));
            float segmentT = t * (lineRenderer.positionCount - 1) - index;

            index = Mathf.Clamp(index, 0, lineRenderer.positionCount - 2);

            return Vector3.Lerp(lineRenderer.GetPosition(index), lineRenderer.GetPosition(index + 1), segmentT);
        }

        /// <summary>
        /// Get the normalized distance (0 to 1) based on a given Vector3 point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public float GetNormalizedDistance(Vector3 point)
        {
            float closestDistance = float.MaxValue;
            float normalizedDistance = 0f;

            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                float distance = Vector3.Distance(lineRenderer.GetPosition(i), point);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    normalizedDistance = i / (float)(lineRenderer.positionCount - 1);
                }
            }

            return normalizedDistance;
        }
        
        public float GetPathAngle(float t)
        {
            if (lineRenderer.positionCount < 2)
            {
                Debug.LogError("At least 2 points are required on the path.");
                return 0f;
            }

            t = Mathf.Clamp01(t); 

            // Get the index for the points to calculate the direction between
            int index = Mathf.FloorToInt(t * (lineRenderer.positionCount - 1));
            float segmentT = t * (lineRenderer.positionCount - 1) - index;

            index = Mathf.Clamp(index, 0, lineRenderer.positionCount - 2);

            // Get two consecutive points on the path
            Vector3 point1 = lineRenderer.GetPosition(index);
            Vector3 point2 = lineRenderer.GetPosition(index + 1);

            // Calculate the direction vector from point1 to point2
            Vector3 direction = (point2 - point1).normalized;

            // Calculate the angle between the direction vector and the forward direction (z-axis)
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 180f;

            return angle;
        }
        
        public float GetPathLength()
        {
            float totalLength = 0f;

            // Loop through each consecutive pair of points to calculate the total path length
            for (int i = 1; i < lineRenderer.positionCount; i++)
            {
                totalLength += Vector3.Distance(lineRenderer.GetPosition(i - 1), lineRenderer.GetPosition(i));
            }

            return totalLength;
        }
    }
}