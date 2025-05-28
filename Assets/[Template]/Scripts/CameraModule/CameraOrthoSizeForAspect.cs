using System;
using System.Collections.Generic;
using UnityEngine;

namespace Template.CameraModule
{
    public class CameraOrthoSizeForAspect : BaseMono, IModuleInit
    {
        public List<AspectOrthoSize> aspectRatios = new List<AspectOrthoSize>();
        public Camera cameraMain;

        public void Init()
        {
            cameraMain.orthographicSize = aspectRatios[0].size;
            foreach (AspectOrthoSize aspect in aspectRatios)
            {
                if (Math.Abs(Math.Round((aspect.width / aspect.height), 2) - Math.Round(cameraMain.aspect, 2)) < .01f)
                {
                    cameraMain.orthographicSize = aspect.size;
                    break;
                }
            }
        }
    }
    
    [Serializable]
    public class AspectOrthoSize
    {
        public float height;
        public float width;
        public float size;
    }
}