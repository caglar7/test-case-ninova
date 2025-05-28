using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Template
{
    [Serializable]
    public class PunchScaleSettings
    {
        public float originalScale;
        public float targetScaleMult;
        public float duration;
        public Ease easeToTarget = Ease.Linear;
        public Ease easeToOriginal = Ease.Linear;
    }
}