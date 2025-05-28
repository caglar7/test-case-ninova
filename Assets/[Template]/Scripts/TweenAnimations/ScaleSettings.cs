using System;
using DG.Tweening;

namespace Template
{
    [Serializable]
    public class ScaleSettings
    {
        public float startScale;
        public float targetScale;
        public float duration;
        public Ease ease = Ease.Linear;
    }
}