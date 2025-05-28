

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Template
{
    public class PoolingPattern
    {
        private GameObject prefab;
        private Stack<GameObject> objPool = new Stack<GameObject>();
        private MonoBehaviour _type;

        public PoolingPattern(GameObject prefab)
        {
            this.prefab = prefab;
        }

        public void FillPool(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                 GameObject obje = Object.Instantiate(prefab);
                obje.hideFlags = HideFlags.HideInHierarchy;
                AddObjToPool(obje);
            }
        }

        public GameObject PullObjFromPool(HideFlags flags = HideFlags.None)
        {
            if (objPool.Count > 0)
            {
                GameObject obje = objPool.Pop();

                if(obje != null) 
                {
                    obje.gameObject.SetActive(true);
                    obje.hideFlags = flags;

                    return obje;  
                }
            }

            GameObject objeIns = Object.Instantiate(prefab);
            objeIns.hideFlags = flags;
            return objeIns;
        }

        public GameObject PullObjFromPool(Vector3 scale, HideFlags flags = HideFlags.None)
        {
            GameObject obj = PullObjFromPool();
            obj.transform.localScale = scale;
            return obj;
        }

        public T PullObjFromPool<T>(HideFlags flags = HideFlags.None) where T : MonoBehaviour
        {
            GameObject obj = PullObjFromPool(flags);
            
            if (obj.TryGetComponent(out _type))
            {
                return _type as T;
            }

            return null;
        }

        public GameObject PullForDuration(float duration = 2f, HideFlags flags = HideFlags.None)
        {
            GameObject obj = PullObjFromPool(flags);

            GeneralUtils.Delay(duration, () => {
                AddObjToPool(obj);
            });

            return obj;
        }

        public T PullForDuration<T>(float duration = 2f, HideFlags flags = HideFlags.None) where T : MonoBehaviour
        {
            GameObject obj = PullObjFromPool(flags);

            if (obj.TryGetComponent(out _type))
            {
                GeneralUtils.Delay(duration, () => {
                    AddObjToPool(obj);
                });

                return _type as T;
            }

            return null;
        }

        public void AddObjToPool(GameObject obje)
        {
            if (obje == null) return;

            if (objPool.Contains(obje))
            {
                Debug.LogWarning("Object is already in the pool");
                return;
            }

            obje.hideFlags = HideFlags.HideInHierarchy;
            obje.transform.DOKill();
            obje.transform.SetParent(null);
            obje.gameObject.SetActive(false);
            objPool.Push(obje);
        }
    }
}
