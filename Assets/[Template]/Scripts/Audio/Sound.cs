using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    [CreateAssetMenu(menuName = "Template/New Sound", fileName = "New Sound")]
    public class Sound : ScriptableObject
    {
        [Header("Settings")]
        public SoundType soundType;
        public AudioClip clip;
        [Range(0.01f, 1f)] public float volume = .1f;
        [Range(0.01f, 10f)] public float basePitch = 1f;
        public bool isLooping = false;
        [HideInInspector] public AudioSource source;
    }
}