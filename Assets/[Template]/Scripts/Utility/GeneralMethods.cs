
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;

namespace Template
{
    public static class GeneralMethods
    {
        // collections
        private static Dictionary<Type, Array> typeArrays = new Dictionary<Type, Array>();

        // delegates
        public delegate void GeneralDelegate();

        public static void Init()
        {
            typeArrays.Clear();
            // moneyDropPoints = UnityEngine.Object.FindObjectsOfType<MoneyDropPoint>(true);

            // typeArrays.Add(typeof(MoneyDropPoint), moneyDropPoints);
        }

        private static T[] GetArray<T>()
        {
            Type type = typeof(T);

            if (typeArrays.ContainsKey(type))
            {
                return typeArrays[type] as T[];
            }

            // Handle unsupported types or throw an exception if needed.
            throw new ArgumentException("Unsupported type");
        }

        public static T FindClosest<T>(Vector3 refPosition, bool includeInactive = false)
        {
            float dis = Mathf.Infinity;
            T found = default(T);

            foreach (T obj in GetArray<T>())
            {
                if (obj is Component component)
                {
                    if (component.gameObject.activeInHierarchy == false)
                    {
                        if (includeInactive == false)
                        {
                            continue;
                        }
                    }

                    float temp = Vector3.Distance(refPosition, component.transform.position);
                    if (temp < dis)
                    {
                        dis = temp;
                        found = obj;
                    }
                }
            }
            return found;
        }

        public static T FindClosest<T>(List<T> listType, Vector3 refPosition)
        {
            float dis = Mathf.Infinity;
            T found = default(T);

            foreach (T obj in listType)
            {
                if (obj is Component component)
                {
                    float temp = Vector3.Distance(refPosition, component.transform.position);
                    if (temp < dis)
                    {
                        dis = temp;
                        found = obj;
                    }
                }
            }
            return found;
        }

        // Helper method to check if a point is within a circle defined by a center and radius
        public static bool IsWithinCircle(Vector3 point, Vector3 center, float radius)
        {
            float squaredDistance = (point - center).sqrMagnitude;

            return squaredDistance <= radius * radius;
        }

        public static Vector3 GetAveragePosition(List<Transform> list)
        {
            Vector3 sum = Vector3.zero;

            foreach (Transform t in list)
                sum += t.position;

            return sum / list.Count;
        }

        public static string GetTimeFromSeconds(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds - (minutes * 60);

            string stringMinutes = "";
            string stringSeconds = "";

            // get minutes 
            if (minutes < 10) stringMinutes = "0" + minutes.ToString();
            else stringMinutes = minutes.ToString();

            // get seconds
            if (seconds < 10) stringSeconds = "0" + seconds.ToString();
            else stringSeconds = seconds.ToString();

            return stringMinutes + ":" + stringSeconds;
        }

        public static RaycastHit[] GetHits()
        {
            return Physics.RaycastAll(GetRay(), 100f);
        }

        public static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        /// <summary>
        /// can be used to drop items anywhere needed
        /// </summary>
        public static void JumpToTarget(Transform item, Vector3 targetPos, PoolingPattern backToPool,
                                            float jumpPower = .5f, float duration = 1f,
                                            Ease ease = Ease.Linear, Action onJumpComplete = null)
        {
            item.DOJump(targetPos, jumpPower, 1, duration).SetEase(ease)
                .OnComplete(() => {

                    onJumpComplete?.Invoke();

                    if(backToPool != null && item != null) backToPool.AddObjToPool(item.gameObject);

                });
        }

        /// <summary>
        /// can be used to drop items anywhere needed
        /// </summary>
        public static void DropItemOnPlace(Transform item, Transform holder, GeneralDelegate onDropDone = null,
                                            PoolingPattern backToPool = null, float jumpPower = .5f, bool isHolderParent = true,
                                            float duration = .5f, int numberOfJumps = 1, Ease ease = Ease.Linear)
        {
            item.SetParent(isHolderParent ? holder : null);

            item.DOJump(holder.position, jumpPower, numberOfJumps, duration).SetEase(ease)
                .OnComplete(() => {

                    onDropDone?.Invoke();

                    if (backToPool != null) backToPool.AddObjToPool(item.gameObject);

                });
        }

        public static T[] ShuffleArray<T>(T[] array)
        {
            // Create a copy of the array to avoid modifying the original array
            T[] newArray = (T[])array.Clone();

            // Shuffle the array using Fisher-Yates shuffle algorithm
            for (int i = newArray.Length - 1; i > 0; i--)
            {
                // Pick a random index before the current element
                int randomIndex = UnityEngine.Random.Range(0, array.Length);

                // Swap the elements
                T temp = newArray[i];
                newArray[i] = newArray[randomIndex];
                newArray[randomIndex] = temp;
            }

            return newArray;
        }

        public static T[] GetRandomItems<T>(T[] array, int count)
        {
            // If count is greater than or equal to the length of the array, return the whole array
            if (count >= array.Length)
            {
                return array;
            }

            T[] randomItems = new T[count];
            T[] shuffledArray = ShuffleArray(array);

            Array.Copy(shuffledArray, randomItems, count);

            return randomItems;
        }

        /// <summary>
        /// get random Vector3 within a sphere
        /// </summary>
        private static Vector3 randomVector3;
        public static Vector3 GetRandomVector(Vector3 center, float radius, Vector3 axis)
        {
            randomVector3.x = center.x + (UnityEngine.Random.Range(-radius, radius) * axis.x);
            randomVector3.y = center.y + (UnityEngine.Random.Range(-radius, radius) * axis.y);
            randomVector3.z = center.z + (UnityEngine.Random.Range(-radius, radius) * axis.z);
            return randomVector3;
        }

        /// <summary>
        /// fade in a given UI object
        /// </summary>
        private static float fadeAlphaValue = 0f;
        public static void FadeUIObject(CanvasGroup canvasGroup, float startAlpha = 0f, float targetAlpha = 1f, 
                                        float duration = .5f, float initDelay = 0f, GeneralDelegate onFadeDone = null)
        {
            if(canvasGroup != null)
            {  
                fadeAlphaValue = startAlpha;
                DOTween.To(() => fadeAlphaValue, y => fadeAlphaValue = y, targetAlpha, duration)
                    .SetUpdate(true)
                    .SetDelay(initDelay)
                    .OnUpdate(() =>
                    {
                        canvasGroup.alpha = fadeAlphaValue;
                    })
                    .OnComplete(() =>
                    {

                        onFadeDone?.Invoke();

                    })
                    .SetId(canvasGroup.transform.name);
            }
        }

        public static T GetRandomEnumValue<T>()
        {
            // Check if the provided type is an enum
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type parameter must be an enum");
            }

            // Get all values of the enum type
            Array enumValues = Enum.GetValues(typeof(T));

            // Get a random index
            int randomIndex = UnityEngine.Random.Range(0, enumValues.Length);

            // Return the random enum value
            return (T)enumValues.GetValue(randomIndex);
        }
        public static T GetRandomEnumValueExcluding<T>(List<T> excludeValues)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Type parameter must be an enum");
            }

            Array enumValues = Enum.GetValues(typeof(T));

            var filteredValues = enumValues.Cast<T>().Where(value => !excludeValues.Contains(value)).ToList();

            if (filteredValues.Count == 0)
            {
                throw new ArgumentException("No valid enum values remaining after exclusion.");
            }

            int randomIndex = UnityEngine.Random.Range(0, filteredValues.Count);

            return filteredValues[randomIndex];
        }

        /// <summary>
        /// check internet connection
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectedToInternet()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                // Connection available
                return true;
            }
            else
            {
                // No connection
                return false;
            }
        }

        public static Vector2 WorldToScreenPosition(Vector3 worldPos, UnityEngine.Camera cam, RectTransform canvasRT)
        {
            // Convert world position to screen position
            Vector3 screenPosition = cam.WorldToScreenPoint(worldPos);

            // Convert screen position to canvas position
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRT, screenPosition, null, out canvasPosition);

            return canvasPosition;
        }
        public static void SortObjectsByX(this List<BaseMono> objs)
        {
            objs.Sort((a, b) => b.Transform.position.x.CompareTo(a.Transform.position.x));
        }
    }
}
