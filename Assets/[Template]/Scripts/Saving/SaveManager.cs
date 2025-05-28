
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// when you try to get any value
/// 
/// get them on Start()
/// 
/// or we can Adjust Script execution so this is called before any other class
/// 
/// because we need Init() called first
/// 
/// </summary>

namespace Template
{
    public class SaveManager : Singleton<SaveManager>
    {
        protected override void Awake()
        {
            base.Awake();

            Init();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private void OnApplicationPause(bool pause)
        {
            SaveGame();
        }

        [SerializeField] public bool isSavingOn;

        public SaveData saveData;
        public List<ISaveLoad> saveObjects = new List<ISaveLoad>();

        private void Init()
        {
            saveObjects.AddRange(FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveLoad>());

            saveData = (isSavingOn == true) ? SaveSystem<SaveData>.Load("SaveData", null) : null;
            if (saveData == null)
            {
                saveData = new SaveData();
                ResetGame();
            }

            LoadGame();
        }

        private void SaveGame()
        {
            if (saveData == null) return;

            foreach (ISaveLoad obj in saveObjects)
            {
                obj.Save(saveData);
            }

            SaveSystem<SaveData>.Save("SaveData", saveData);
        }

        private void LoadGame()
        {
            if (saveData == null) return;

            foreach(ISaveLoad obj in saveObjects)
            {
                obj.Load(saveData);
            }
        }

        private void ResetGame()
        {
            if (saveData == null) return;

            foreach (ISaveLoad obj in saveObjects)
            {
                obj.Reset(saveData);
            }

            SaveSystem<SaveData>.Save("SaveData", saveData);
        }
    }
}

