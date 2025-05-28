using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Template
{
    [CreateAssetMenu(fileName = "New Pool Object", menuName = "Template/New Pool Object")]
    public class PoolObject : ScriptableObject
    {
        public GameObject obj;
        public string poolName;
        public int poolCount;
        [HideInInspector] public PoolingPattern poolingPattern;
    }
}