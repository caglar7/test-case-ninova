using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Template;

/// <summary>
/// currentlly used for calling the first tutorial
/// 
/// also will be responsible for saving and loading the tutorials
/// 
/// </summary>

namespace Template
{
    public class TutorialManager : MonoBehaviour, ISaveLoad
    {
        [Header("All Tutorial Units")]
        public TutorialUnit[] tutorialUnits;

        public void Reset(SaveData saveData)
        {
            saveData.tutorials = new bool[12];

            for (int i = 0; i < saveData.tutorials.Length; i++)
            {
                saveData.tutorials[i] = false;
            }
        }

        public void Load(SaveData saveData)
        {
            for (int i = 0; i < tutorialUnits.Length; i++)
            {
                tutorialUnits[i].isShown = saveData.tutorials[i];
            }
        }

        public void Save(SaveData saveData)
        {
            for (int i = 0; i < tutorialUnits.Length; i++)
            {
                saveData.tutorials[i] = tutorialUnits[i].isShown;
            }
        }

        private void Start()
        {
            // set indexes
            for (int i = 0; i < tutorialUnits.Length; i++)
            {
                tutorialUnits[i].tutorialIndex = i;
            }

            // first tutorial
            if (tutorialUnits.Length > 0 && tutorialUnits[0].isShown == false)
            {
                tutorialUnits[0].ShowTutorial();
            }
        }
    }
}