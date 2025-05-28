using System.Collections.Generic;
using Template;
using UnityEngine;

namespace _Template_.Scripts.LevelModule
{
    public class PrefabLevelManager : BaseLevelManager
    {
        [Header("Level Prefabs")]
        public List<GameObject> levelPrefabs = new List<GameObject>();

        private GameObject _currentLevelPrefab;
        
        protected override int GetLevelCount()
        {
            return levelPrefabs.Count;
        }

        public override void UnLoadCurrentLevel()
        {
            if(_currentLevelPrefab is not null)
                Destroy(_currentLevelPrefab);
        }

        public override void LoadLevelWithIndex(int index)
        {
            base.LoadLevelWithIndex(index);
            
            _currentLevelPrefab = Instantiate(levelPrefabs[index]);
        }

        protected override bool IsThereLevelOnEditor(out int levelIndex)
        {
            levelIndex = 0;

            LevelPrefab levelPrefab = FindObjectOfType<LevelPrefab>();
            
            if (levelPrefab is not null)
            {   
                levelIndex = levelPrefab.index;
                _currentLevelPrefab = levelPrefab.gameObject;
                return true;
            }
            return false;
        }
    }
}