using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Template
{
    public static class PhysicsUtility
    {
        public static bool AreCollidersIntersect(Collider colA, Collider colB)
        {
            return colA.bounds.Intersects(colB.bounds);
        }

        public static bool IsColliderOverlappingOthers(BoxCollider objCollider, Transform objTransform)
        {
            bool isOverlapping = false;
            
            foreach (Collider collider in 
                    Physics.OverlapBox(objTransform.position, objCollider.size / 2f, objTransform.rotation))
            {
                if (collider.CompareTag("InputBlock"))
                {
                    continue;
                }
                
                isOverlapping = true;
            }

            return isOverlapping;
        }
    }
}