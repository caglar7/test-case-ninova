using System;
using DG.Tweening;
using UnityEngine.Serialization;

namespace Template
{
    [Serializable]
    public class TweenRotationSettings
    {
        public float duration = 1f;
        public Ease ease = Ease.Linear;
    }
}