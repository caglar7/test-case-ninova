using System;
using System.Collections.Generic;
using Template;
using UnityEngine;
using UnityEngine.Serialization;

namespace Template
{
    [Serializable]
    public class ObjectTrailData
    {
        [Header("Base")] 
        public int maxVisualCount = 32;
        public float objScale = 1f;
        public float tweenDuration = .3f;
        public bool generateRandomly = false;
    }

}