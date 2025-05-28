using System;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Template
{
    public static class TweenAnimation
    {
        public static void PunchScalePreset(Transform obj)
        {
            PunchScale(obj, GlobalAnimationSettings.Instance.punchScale);
        }
        public static void PunchScalePreset(Transform obj, Action onComplete)
        {
            PunchScale(obj, GlobalAnimationSettings.Instance.punchScale, onComplete);
        }
        
        public static void PunchScale(Transform obj, PunchScaleSettings punchScaleSettings, Action onComplete = null)
        {
            obj.DOKill();
            obj.transform.localScale = Vector3.one * punchScaleSettings.originalScale;
            Vector3 startScale = obj.transform.localScale;
            
            obj.DOScale(startScale * punchScaleSettings.targetScaleMult, punchScaleSettings.duration * .5f)
            .SetEase(punchScaleSettings.easeToTarget)
            .OnComplete(() =>
            {
                obj.DOScale(startScale, punchScaleSettings.duration * .5f)
                .SetEase(punchScaleSettings.easeToOriginal)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
            });
        }

        public static void Scale(Transform obj, ScaleSettings settings, Action onComplete = null)
        {
            obj.localScale = settings.startScale * Vector3.one;
            
            obj.DOScale(settings.targetScale, settings.duration)
                .SetEase(settings.ease)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });        
        }

        public static void Rotate360(Transform obj)
        {
            obj.DOLocalRotate(new Vector3(360f, 0, 0), .5f, RotateMode.LocalAxisAdd);
        }

        public static void ShakePosition(Transform obj, ShakeSettings shakeSettings)
        {
            if (DOTween.IsTweening(obj))
                return;
            
            obj.DOShakePosition(
                shakeSettings.duration,
                shakeSettings.strength,
                shakeSettings.vibrato,
                shakeSettings.randomness,
                shakeSettings.snapping,
                shakeSettings.fadeOut
            );
        }
        public static void ShakeRotation(Transform obj, ShakeSettings shakeSettings)
        {
            if (DOTween.IsTweening(obj))
                return;
            
            obj.DOShakeRotation(
                shakeSettings.duration,
                shakeSettings.strength,
                shakeSettings.vibrato,
                shakeSettings.randomness,
                shakeSettings.fadeOut
            );
        }
    }
}