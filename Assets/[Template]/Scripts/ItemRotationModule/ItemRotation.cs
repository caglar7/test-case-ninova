using System;
using DG.Tweening;
using UnityEngine;

namespace Template
{
    public static class ItemRotation
    {
        public static void RotateGlobal(
            BaseMono obj, 
            Vector3 targetRot, 
            TweenRotationSettings rotationSettings,
            Action onComplete = null)
        {
            obj.Transform.DORotate(
                targetRot,
                rotationSettings.duration)
                .SetEase(rotationSettings.ease)
                .OnComplete((() =>
                    {
                        onComplete?.Invoke();
                    })
                );
        }
    }
}