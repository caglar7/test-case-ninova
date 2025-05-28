using UnityEngine;
using DG.Tweening;
using System;

namespace Template
{
    public static class DotweenExtensions
    {
        #region Delegates

        public delegate void ActivityDo();

        #endregion

        /// <summary>
        /// It's safer than normal DoPunchScale. Because it will always keep the original scale on consecutive callbacks.
        /// </summary>
        /// <param name="transform">Transform which dotween will be applied.</param>
        /// <param name="targetScale">Target scale of transform</param>
        /// <param name="originalScale">Original scale of transform</param>
        /// <param name="duration">Total duraion of tween</param>
        public static void SafePunchScale(this Transform transform, float targetScale = 1.2f, float originalScale = 1f, float duration = 0.15f, ActivityDo onComplete = null, ActivityDo onGrowthComplete = null)
        {
            transform.DOScale(originalScale, duration * 0.1f).OnComplete(() =>
            {
                transform.DOScale(targetScale, duration * 0.45f).OnComplete(() =>
                {
                    onGrowthComplete?.Invoke();

                    transform.DOScale(originalScale, duration * 0.45f).OnComplete(() =>
                    {
                        onComplete?.Invoke();
                    });
                });
            });
        }

        public static void SafePunchScale(this Transform transform, Vector3 targetScale, Vector3 originalScale, float duration = 0.15f, ActivityDo onComplete = null, ActivityDo onGrowthComplete = null)
        {
            transform.DOScale(originalScale, duration * 0.1f).OnComplete(() =>
            {
                transform.DOScale(targetScale, duration * 0.45f).OnComplete(() =>
                {
                    onGrowthComplete?.Invoke();

                    transform.DOScale(originalScale, duration * 0.45f).OnComplete(() =>
                    {
                        onComplete?.Invoke();
                    });
                });
            });
        }

        public static void SafePunchRotation(this Transform transform, Vector3 originalRot, Vector3 extraRotation, float duration = 0.15f, ActivityDo onComplete = null)
        {
            transform.DORotate(originalRot, duration * 0.1f).OnComplete(() =>
            {
                transform.DORotate(originalRot + extraRotation, duration * 0.45f).OnComplete(() =>
                {
                    transform.DORotate(originalRot, duration * 0.45f).OnComplete(() =>
                    {
                        onComplete?.Invoke();
                    });
                });
            });
        }

        public static void SafePunchLocalRotation(this Transform transform, Vector3 originalRot, Vector3 extraRotation, float duration = 0.15f, ActivityDo onComplete = null)
        {
            transform.DOLocalRotate(originalRot, duration * 0.1f).OnComplete(() =>
            {
                transform.DOLocalRotate(originalRot + extraRotation, duration * 0.45f).OnComplete(() =>
                {
                    transform.DOLocalRotate(originalRot, duration * 0.45f).OnComplete(() =>
                    {
                        onComplete?.Invoke();
                    });
                });
            });
        }

        public static void SafePunchColor(this Material mat, Color originalColor, Color targetColor, float duration = 0.15f, ActivityDo onComplete = null)
        {
            mat.DOColor(originalColor, duration * 0.1f).OnComplete(() =>
            {
                mat.DOColor(targetColor, duration * 0.45f).OnComplete(() =>
                {
                    mat.DOColor(originalColor, duration * 0.45f).OnComplete(() =>
                    {
                        onComplete?.Invoke();
                    });
                });
            });
        }

        public static void OnButtonPressed()
        {
            GameObject selectedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

            if (selectedButton != null)
            {
                SafePunchScale(selectedButton.transform);
                SafePunchLocalRotation(selectedButton.transform, Vector3.zero, Vector3.forward * 10);
            }
        }


        /// <summary>
        /// Moves to target point and bounces back numJump times. Every bounce back value is smaller than before.
        /// </summary>
        /// <param name="totalDuration">Total duration including every bounce backs, not duration per jump!</param>
        /// <param name="numJump">Number of bounce backs that will occur.</param>
        /// <param name="jumpPercentNormalized">This value needs to be between [0, 1].<br>Next, the bounce back value is 
        /// calculated according to this float. The bigger the value, the closer the transform will move to the 
        /// start position instead of the target position during bounce back.</br></param>
        /// <param name="isInitialLoop">"DON'T CHANGE THIS VARIABLE."</param>
        /// <param name="durationOfThisLoop">"DON'T CHANGE THIS VARIABLE."</param>
        public static void MoveOutBack(this Transform transform, Vector3 targetPos, float totalDuration, int numJump, float jumpPercentNormalized, ActivityDo onHitTarget = null, ActivityDo onComplete = null, bool isInitialLoop = true, float durationOfThisLoop = 0)
        {
            // Calculate duration of initial loop.
            if (isInitialLoop)
            {
                if (jumpPercentNormalized < 0 || jumpPercentNormalized > 1)
                    throw new Exception("jumpPercentNormalized must be between [0, 1]");

                isInitialLoop = false;
                float constant = 1 / jumpPercentNormalized; // c = 1 / jumpPercentNormalized
                float serialAddition = 0;

                for (int i = 1; i <= numJump; i++) // serialAddition = (c ^ (n-1) + c ^ (n-2) + ... + c ^ 0)
                {
                    serialAddition += Mathf.Pow(constant, numJump - i);
                }
                durationOfThisLoop = ((Mathf.Pow(constant, numJump - 1) * totalDuration) / serialAddition); // (c ^ (n-1) * t) / serialAddition
            }

            Vector3 bouncePos = Vector3.Lerp(targetPos, transform.position, jumpPercentNormalized);
            transform.DOMove(targetPos, durationOfThisLoop * (1 / (1 + jumpPercentNormalized))).SetEase(Ease.InQuart).OnComplete(() => // (1 / (1 + jumpPercentNormalized)) => lifePercent of first phase
            {
                onHitTarget?.Invoke();

                numJump--; // calculate remainder numJump
                if (numJump > 0)
                {
                    transform.DOMove(bouncePos, durationOfThisLoop * (jumpPercentNormalized / (1 + jumpPercentNormalized))).SetEase(Ease.OutQuart).OnComplete(() => // (jumpPercentNormalized / (1 + jumpPercentNormalized)) => lifePercent of second phase
                    {
                        MoveOutBack(transform, targetPos, totalDuration, numJump, jumpPercentNormalized, onHitTarget, onComplete, false, durationOfThisLoop * jumpPercentNormalized);
                    });
                }
                else
                {
                    if (onComplete != null)
                        onComplete();
                }
            });
        }
    }

}