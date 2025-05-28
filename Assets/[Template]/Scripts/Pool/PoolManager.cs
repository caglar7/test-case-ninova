

using System.Collections.Generic;
using GAME;
using UnityEngine;
using UnityEngine.Serialization;

namespace Template
{
    public class PoolManager : Singleton<PoolManager>, IModuleInit
    {
        [Header("Other Pools")]
        public PoolObject[] basePoolObjects;

        [Header("Fields")]
        public PoolingPattern[] poolingPatterns;

        private List<PoolObject> allPoolObjects = new List<PoolObject>();

        /// <summary>
        /// init with the given array of pool objects
        /// </summary>
        public void Init()
        {
            allPoolObjects.AddRange(basePoolObjects);

            poolingPatterns = new PoolingPattern[allPoolObjects.Count];

            for (int i = 0; i < allPoolObjects.Count; i++)
            {
                PoolingPattern pattern = new PoolingPattern(allPoolObjects[i].obj);
                pattern.FillPool(allPoolObjects[i].poolCount);
                poolingPatterns[i] = pattern;
                allPoolObjects[i].poolingPattern = pattern;
            }
        }

        /// <summary>
        /// try get pool if there is a pool with the given name string
        /// </summary>
        /// <param name="poolName"></param>
        /// <param name="pool"></param>
        /// <returns></returns>
        public bool TryGetPool(string poolName, PoolingPattern cashedPool, out PoolingPattern pool)
        {
            bool didGetPool = false;

            if(cashedPool != null)
            {
                pool = cashedPool;
                return true;
            }
            else pool = null;

            for (int i = 0; i < allPoolObjects.Count; i++)
            {
                if(poolName == allPoolObjects[i].poolName)
                {
                    pool = poolingPatterns[i];
                    didGetPool = true;
                    break;
                }
            }

            if (didGetPool == false) Debug.LogWarning("No Pool found with given name => " + poolName);

            return didGetPool;
        }
    }
}
