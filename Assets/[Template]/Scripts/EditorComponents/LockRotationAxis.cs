using System;
using UnityEngine;

namespace Template.EditorComponents
{
    
    [ExecuteInEditMode]
    public class LockRotationAxis : BaseMono
    {
        public bool lockX = false;
        public bool lockY = false;
        public bool lockZ = false;
        public float lockValue = 0f;
        
        private void Update()
        {
            if (Application.isPlaying) 
                return;
            
            if (lockX)
            {
                Transform.eulerAngles = new Vector3(lockValue, Transform.eulerAngles.y, Transform.eulerAngles.z);
            }

            if (lockY)
            {
                Transform.eulerAngles = new Vector3(Transform.eulerAngles.x, lockValue, Transform.eulerAngles.z);
            }

            if (lockZ)
            {
                Transform.eulerAngles = new Vector3(Transform.eulerAngles.x, Transform.eulerAngles.y, lockValue);
            }
        }
    }
}