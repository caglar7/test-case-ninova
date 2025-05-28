
using UnityEngine;

namespace Template
{
    public static class VectorUtility
    {
        /// <summary>
        /// Axis can be => (1, 0, 0), (0, 1, 0) or (0, 0, 1)
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="axis"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector3 ChangeOneAxis(Vector3 vec, Vector3 axis, float value)
        {
            if(axis.x == 1) vec.x = value;
            if(axis.y == 1) vec.y = value;
            if(axis.z == 1) vec.z = value;
            return vec;
        }

        public static Vector3 GetSelectedAxisFromTarget(Vector3 currentVector, Vector3 targetVector, Vector3 axis)
        {
            if(axis.x == 1) currentVector.x = targetVector.x;
            if(axis.y == 1) currentVector.y = targetVector.y;
            if(axis.z == 1) currentVector.z = targetVector.z;
            return currentVector;
        }

        private static Vector3 normalizedDirection, right, forward; 
        public static Vector3 GetLocalXZ(Vector3 localDirection, float localX, float localZ)
        {
            normalizedDirection = localDirection.normalized;

            right = new Vector3(normalizedDirection.z, 0, -normalizedDirection.x);
            
            forward = new Vector3(normalizedDirection.x, 0, normalizedDirection.z);

            return (right * localX) + (forward * localZ);
        }

        private static Quaternion rot;
        public static Vector3 RotateAroundY(Vector3 vector, float angle)
        {
            rot = Quaternion.Euler(0, angle, 0);

            return rot * vector;
        }
    }
}