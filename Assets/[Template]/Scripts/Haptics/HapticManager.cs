using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// currently using
/// 
/// Taptic
/// 
/// </summary>

namespace Template
{
    public class HapticManager : Singleton<HapticManager>, ISaveLoad
    {
        #region Singleton
        protected override void Awake()
        {
            base.Awake();
        }
        #endregion

        public bool isHapticEnabled;

        public void EnableHaptic(bool isEnabled)
        {
            isHapticEnabled = isEnabled;
        }

        public void Medium()
        {
            if (isHapticEnabled == false) return;

            //Taptic.Medium();
        }

        #region Save Load
        public void Reset(SaveData saveData)
        {
            saveData.isHapticEnabled = false;
        }

        public void Load(SaveData saveData)
        {
            isHapticEnabled = saveData.isHapticEnabled;
        }

        public void Save(SaveData saveData)
        {
            saveData.isHapticEnabled = isHapticEnabled;
        }
        #endregion
    }
}