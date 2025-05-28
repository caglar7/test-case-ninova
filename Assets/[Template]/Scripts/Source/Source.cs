using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    [CreateAssetMenu(menuName = "Template/Create New Source", fileName = "New Source")]
    public class Source : ScriptableObject
    {
        [Header("Source Settings")] 
        public SourceType sourceType;
        public Sprite icon;
        public float initialValue;
        public float currentValue;
    }
}