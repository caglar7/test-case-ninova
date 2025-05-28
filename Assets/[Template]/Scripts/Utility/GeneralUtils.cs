using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Template
{
    public static class GeneralUtils
    {
        public delegate void ActivityGeneral();

        #region Delay

        public static void Delay(float duration, ActivityGeneral delayedActivity, bool useUnscaledTime = false)
        {
            DOVirtual.DelayedCall(duration, () =>
            {
                delayedActivity();

            }).SetUpdate(useUnscaledTime);
            //DOTween.To(() => 0, x => x = 0, 0, duration).OnComplete(() => { delayedActivity(); });
        }

        public static void Delay(float duration, ActivityGeneral delayedActivity, ActivityGeneral onUpdate)
        {
            DOTween.To(() => 0, x => x = 0, 0, duration).
                OnComplete(() => { delayedActivity(); }).
                OnUpdate(() => { onUpdate?.Invoke(); });
        }

        public static IEnumerator DelayCo(float delay, ActivityGeneral delayedActivity)
        {
            yield return new WaitForSeconds(delay);

            delayedActivity();
        }

        #endregion

        #region Vector Calculations

        /// <summary>
        /// for the axis vector3, use 0 for ignoring an axis, 1 for including
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <param name="axis"></param>
        public static float DistanceAxis(Vector3 pos1, Vector3 pos2, Vector3 axis)
        {
            pos1 = new Vector3(pos1.x * axis.x, pos1.y * axis.y, pos1.z * axis.z);
            pos2 = new Vector3(pos2.x * axis.x, pos2.y * axis.y, pos2.z * axis.z);

            return Vector3.Distance(pos1, pos2);
        }

        public static Vector3 RandomVector3(float range)
        {
            float x = Random.Range(-range, range);
            float y = Random.Range(-range, range);
            float z = Random.Range(-range, range);

            return new Vector3(x, y, z);
        }
        #endregion
    }
}
