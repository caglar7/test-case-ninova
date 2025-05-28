using System;
using UnityEngine;

namespace Template
{
    [Serializable]
    public class VFXData
    {
        public VFXType vfxType;
        public PoolObject poolObject;
        public float stayDuration;
        public Vector3 spawnPositionOffset;
    }
}