using System;
using UnityEngine;

namespace Template.CameraModule
{
    public class LookAtCamera : BaseMono
    {
        [Header("References")]
        public Camera cam;

        [Header("Settings")] 
        public bool callOnStart = true;
        public bool callOnUpdate = false;

        private void Start()
        {
            if (callOnStart)
            {
                Apply();
            }
        }

        private void Update()
        {
            if (callOnUpdate)
            {
                Apply();
            }
        }

        private void Apply()
        {
            Transform.LookAt(cam.transform);
        }
    }
}