using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Template.CameraModule
{
    public class CameraFOVForAspect : BaseMono, IModuleInit
    {
        public List<AspectFOV> aspectRatios = new List<AspectFOV>();

        public Camera cameraMain;

        public void Init()
        {
            foreach (AspectFOV aspect in aspectRatios)
            {
                if (Math.Abs(Math.Round((aspect.width / aspect.height), 2) - Math.Round(cameraMain.aspect, 2)) < .01f)
                {
                    cameraMain.fieldOfView = aspect.fov;
                    break;
                }
            }
        }
    }

    [Serializable]
    public class AspectFOV
    {
        public float height;
        public float width;
        public float fov;
    }
}