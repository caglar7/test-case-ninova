using System;

namespace Template
{
    [Serializable]
    public class ShakeSettings
    {
        public float duration = 1f;
        public float strength = 90f;
        public int vibrato = 10;
        public float randomness = 90f;
        public bool snapping = false;
        public bool fadeOut = true;
    }
}