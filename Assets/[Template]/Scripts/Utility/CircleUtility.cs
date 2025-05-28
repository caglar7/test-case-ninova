
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public static class CircleUtility
    {
        public static Vector2 GetPointAroundCircle(Vector2 centerPosition, float radius, float angle)
        {
            // Convert angle from degrees to radians
            float angleInRadians = angle * (float) Math.PI / 180f;

            // Calculate the x and y coordinates of the point on the circle
            float x = centerPosition.x + radius * (float) Math.Cos(angleInRadians);
            float y = centerPosition.y + radius * (float) Math.Sin(angleInRadians);

            return new Vector2(x, y);
        }
        
        public static Vector3 GetPointAroundCircle(Vector3 centerPosition, float radius, float angle)
        {
            // Convert angle from degrees to radians
            float angleInRadians = angle * (float) Math.PI / 180f;

            // Calculate the x and y coordinates of the point on the circle
            float x = centerPosition.x + radius * (float) Math.Cos(angleInRadians);
            float z = centerPosition.z + radius * (float) Math.Sin(angleInRadians);

            return new Vector3(x, centerPosition.y, z);
        }

        /// <summary>
        /// uniformly distributed points around the circle
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="radius"></param>
        /// <param name="numOfPoints"></param>
        /// <returns></returns>
        public static List<Vector2> GetPointsAroundCircle(Vector2 origin, float radius, int numOfPoints)
        {
            List<Vector2> points = new List<Vector2>();

            // Calculate angle increment for each point
            float angleIncrement = 360f / numOfPoints;


            // Generate points evenly distributed around the circle
            float angle = 90f;
            for (int i = 0; i < numOfPoints; i++)
            {
                points.Add(GetPointAroundCircle(origin, radius, angle));
                angle += angleIncrement;
            }

            return points;
        }

        /// <summary>
        /// angle referenced to the Vector2.right on a Unit Circle
        /// 
        /// in 0-360 range
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static float GetAngleInUnitCircle(Vector3 dir)
        {
            // Convert Vector3 to Vector2 for 2D calculations
            Vector2 vA = new Vector2(dir.x, dir.y);

            // Calculate the angle between Vector2.right and the vector A
            float angle = Vector2.Angle(Vector2.right, vA);

            // Determine the sign of the angle
            float crossProduct = Vector3.Cross(Vector2.right, dir).z;
            if (crossProduct < 0)
            {
                angle = 360 - angle;
            }

            return angle;
        }

        // Method to check if an angle is within a circular range
        public static bool IsAngleInRange(float angle, float minAngle, float maxAngle)
        {
            // Normalize angles to be within [0, 360) degrees
            angle = MathUtility.NormalizeAngle(angle);
            minAngle = MathUtility.NormalizeAngle(minAngle);
            maxAngle = MathUtility.NormalizeAngle(maxAngle);

            // If the range is simple (i.e., minAngle <= maxAngle)
            if (minAngle <= maxAngle)
            {
                return angle > minAngle && angle <= maxAngle;
            }
            // If the range wraps around 360 degrees
            else
            {
                return angle > minAngle || angle <= maxAngle;
            }
        }
    }
}
