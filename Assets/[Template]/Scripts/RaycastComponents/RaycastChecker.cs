using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Template
{
    public class RaycastChecker : BaseMono
    {
        public List<Transform> originPoints = new List<Transform>();
        public Vector3 rayDirection;
        public float rayDistance;

        public bool IsThereAnyObject()
        {
            foreach (Transform origin in originPoints)
            {
                if (IsObjectInRay(origin, rayDirection, rayDistance))
                {
                    return true;
                }
            }
            return false;
        }


        public bool IsThereAnyObjectLocal(out float hitDistance)
        {
            foreach (Transform origin in originPoints)
            {
                if (IsObjectInRay(origin, origin.forward, rayDistance, out hitDistance))
                {
                    print("there is");
                    return true;
                }
            }
            print("there is no");
            hitDistance = 0f;
            return false;
        }

        // refactor
        public bool IsThereObjectWithType<T>( out T typeFound) 
        where T : class
        {
            if (Physics.Raycast(originPoints[0].position, originPoints[0].forward, out RaycastHit hit, rayDistance))
            {
                typeFound = hit.collider.GetComponent<T>();
                return true;
            }

            typeFound = null;
            return false;
        }
        
        public bool TryGetObjectsInDirection<T>(Vector3 dir, out List<T> objs) where T : class
        {
            objs = new List<T>();

            for (int i = 0; i < originPoints.Count; i++)
            {
                if (TryGetObjectInDirection(i, dir, out T obj))
                {
                    objs.Add(obj);
                }
            }

            if (objs.Count > 0)
                return true;
            
            return false;
        }
        
        public bool TryGetObjectInDirection<T>(int index, Vector3 dir, out T objFound) where T : class
        {
            if (Physics.Raycast(originPoints[index].position, dir, out RaycastHit hit))
            {
                objFound = hit.collider.GetComponent<T>();
                return true;
            }

            objFound = null;
            return false;
        }
        
        private bool IsObjectInRay(Transform originTransform, Vector3 direction, float dis)
        {
            RaycastHit hit;
            if (Physics.Raycast(originTransform.position, direction, out hit, dis))
            {
                return true;
            }
            return false;
        }
        
        private bool IsObjectInRay(Transform originTransform, Vector3 direction, float dis, out float hitDistance)
        {
            hitDistance = 0f;
            RaycastHit hit;
            if (Physics.Raycast(originTransform.position, direction, out hit, dis))
            {
                hitDistance = Vector3.Distance(hit.point, originTransform.position);
                return true;
            }
            return false;
        }
        
        
    }
}