
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Linq;
using GAME;
using Sirenix.OdinInspector;

namespace Template
{
    public abstract class BaseLevelManager : BaseMono, IModuleInit
    {
        [Header("Settings")]
        public LevelManagerData settings;

        [Header("Fields")]
        public int originalLevel = 1;
        public int fakeLevel = 1;

        protected int _currentLevelIndex = -1;

        public virtual void Init()
        {
            if (IsThereLevelOnEditor(out int levelIndex))
            {
                _currentLevelIndex = levelIndex;
                originalLevel = levelIndex + 1;
                fakeLevel = levelIndex + 1;
                LevelEvents.OnLevelLoaded?.Invoke(fakeLevel);
            }
            else LoadCurrentLevel();
        }
        
        [Button]
        public void LoadNextLevel()
        {
            originalLevel++;
            fakeLevel++;

            if (settings.randomAfterLast == true && fakeLevel > GetLevelCount())
                originalLevel = Random.Range(settings.resetLevel, GetLevelCount() + 1);

            else if (settings.randomAfterLast == false && originalLevel > GetLevelCount())
                originalLevel = settings.resetLevel;

            if(_currentLevelIndex != -1)
            {
                UnLoadCurrentLevel();
            }

            LoadLevelWithIndex(originalLevel - 1);
        }

        public void ReLoadCurrentLevel()
        {
            UnLoadCurrentLevel();
            LoadCurrentLevel();
        }

        // SceneLoader.instance.UnLoadScene(levels[currentLevelIndex]); // sub
        public abstract void UnLoadCurrentLevel();
        
        public void LoadCurrentLevel()
        {
            LoadLevelWithIndex(originalLevel - 1);
        }

        public virtual void LoadLevelWithIndex(int index)
        {
            _currentLevelIndex = index;

            LevelEvents.OnLevelLoaded?.Invoke(fakeLevel);
            
            // SceneLoader.instance.LoadScene(levels[level]); // sub
        }
        protected abstract int GetLevelCount();
        protected abstract bool IsThereLevelOnEditor(out int levelIndex);


        // Get a good save module later on, json of course
        // public void Reset(SaveData saveData)
        // {
        //     saveData.originalLevel = 1;
        //     saveData.fakeLevel = 1;
        // }
        //
        // public void Load(SaveData saveData)
        // {
        //     originalLevel = saveData.originalLevel;
        //     fakeLevel = saveData.fakeLevel;
        // }
        //
        // public void Save(SaveData saveData)
        // {
        //     saveData.originalLevel = originalLevel;
        //     saveData.fakeLevel = fakeLevel;
        // }
    }
}


