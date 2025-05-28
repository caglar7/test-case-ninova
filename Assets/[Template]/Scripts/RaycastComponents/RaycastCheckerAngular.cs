using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.RaycastComponents
{
    public class RaycastCheckerAngular : BaseMono
    {
        public float checkDistance = 1f;
        
        public bool TryGetType<T>(Vector3 origin, float angleAroundZ, out T obj) where T : MonoBehaviour
        {
            obj = null;
            
            Quaternion rotation = Quaternion.Euler(0, 0, angleAroundZ);
            
            Vector3 direction = rotation * Vector3.right; // Right direction in local space

            if (Physics.Raycast(origin, direction, out RaycastHit hit, checkDistance))
            {
                if (hit.collider.TryGetComponent<T>(out obj))
                {
                    return true;
                }
            }
            return false; 
        }
    }
}