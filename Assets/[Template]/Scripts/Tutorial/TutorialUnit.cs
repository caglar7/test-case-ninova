using System.Collections;
using System.Collections.Generic;
using Template;
using UnityEngine;
using UnityEditor;

namespace Template
{
    [CreateAssetMenu(fileName = "New Tutorial Unit", menuName = "Tutorial Unit")]
    public class TutorialUnit : ScriptableObject
    {
        [Header("Settings")]
        public bool isShown = false;
        public float initialDelay = 1f;
        public float stayTime = 1f;
        public string text = "";

        public int tutorialIndex;

        public void ShowTutorial()
        {
            isShown = true;

        }
    }
}