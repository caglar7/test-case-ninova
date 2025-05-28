using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Template
{
    public class ObjectAnimator : BaseMono
    {
        [Header("Shake Settings")] 
        public float shakeDuration = 1f;
        public float shakeStrength = 90;
        public int vibrato = 10;
        public float shakeRandomness = 90;
        
        [Header("Bounce Settings")]
        public float bounceDuration1 = 1f;
        public float bounceDuration2 = 1f;
        public float bounceSmallScale = .5f;
        public float bounceBigScale = 1.5f;
        public Ease bounceEase1 = Ease.Linear;
        public Ease bounceEase2 = Ease.Linear;

        [Header("Scale Up Down Settings")] 
        public float scaleUp = 1.5f;
        public float scaleUpTime = .5f;
        public float scaleResetTime = .5f;
        public Ease scaleUpEase = Ease.Linear;
        public Ease scaleResetEase = Ease.Linear;
        
        [Header("Shake Forever Settings")]
        public float shakeDurationFo = 1f;
        public float shakeStrengthFo = 90;
        public int vibratoFo = 10;
        public int shakeRandomnessFo = 90;
        
        [Header("References")]
        public Transform graphic;

        private bool _canShake = true;
        private bool _canScaleUp = true;
        
        [Button]
        public void Shake(Action onDone = null)
        {
            if (_canShake == false)
                return;
            
            _canShake = false;
            
            graphic.DOShakeRotation(
                shakeDuration, 
                shakeStrength, 
                vibrato, 
                shakeRandomness, 
                true)
                .OnComplete((() =>
                {
                    _canShake = true;
                    onDone?.Invoke();
                }));
        }

        
        [Button]
        public void Bounce(Action onDone = null)
        {
            Vector3 finalScale = graphic.localScale;

            graphic.localScale = finalScale * bounceSmallScale;
            
            graphic.DOScale(
                finalScale * bounceBigScale,
                bounceDuration1)
                .SetEase(bounceEase1)
                .OnComplete((() =>
                {
                    graphic.DOScale(
                        finalScale,
                        bounceDuration2)
                        .SetEase(bounceEase2)
                        .OnComplete(() =>
                        {
                            onDone?.Invoke();
                        });
                }));
        }

        [Button]
        public void ScaleUpDown(Action onDone = null)
        {
            if (_canScaleUp == false)
                return;
            
            _canScaleUp = false;
            
            Vector3 scaleReset = graphic.localScale;
            
            graphic.DOScale(
                Vector3.one * scaleUp,
                scaleUpTime)
                .SetEase(scaleUpEase)
                .OnComplete((() =>
                {
                    graphic.DOScale(
                        scaleReset,
                        scaleResetTime)
                        .SetEase(scaleResetEase)
                        .OnComplete(() =>
                        {
                            _canScaleUp = true;
                            onDone?.Invoke();
                        });
                }));
        }
        
        [Button]
        public void ShakeForever()
        {
            graphic.DOShakePosition(
                    shakeDurationFo, 
                    new Vector3(shakeStrengthFo, shakeStrengthFo, shakeStrengthFo),
                    vibratoFo, 
                    shakeRandomnessFo, 
                    false)
                .OnComplete((() =>
                {
                    // _canShake = true;
                    ShakeForever();
                }));
        }

        [Button]
        public void StopShake()
        {
            graphic.DOKill();
        }
    }
}