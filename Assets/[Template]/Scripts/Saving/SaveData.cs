using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Template
{
    [Serializable]
    public class SaveData
    {
        // level progress
        public int originalLevel, fakeLevel;

        // source
        public float[] sources;

        // audio and haptic
        public bool isAudioEnabled;
        public bool isHapticEnabled;

        // tutorials
        public bool[] tutorials;
    }
}