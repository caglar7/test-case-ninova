using System.Collections.Generic;
using _GAME.Scripts.ItemModule;
using Template;
using UnityEngine;

namespace Template
{
    public class VFXManager : BaseMono
    {
        [Header("VFX List")]
        public List<VFXData> allVFX = new List<VFXData>();
        
        private GameObject _vfxObject;
        private VFXData _currentVFX;
        
        public void Spawn(VFXType vfxType, Vector3 spawnPosition, Vector3 eulerAngles = default)
        {
            _currentVFX = GetVFX(vfxType);
            _vfxObject = _currentVFX.poolObject.poolingPattern.PullForDuration(_currentVFX.stayDuration);
            _vfxObject.transform.position = spawnPosition + _currentVFX.spawnPositionOffset;
            _vfxObject.transform.eulerAngles = eulerAngles;
        }
        
        public VFXData GetVFX(VFXType vfxType)
        {
            foreach (var vfxData in allVFX)
            {
                if (vfxData.vfxType == vfxType)
                {
                    return vfxData;
                }
            }
            return null;
        }
    }
}