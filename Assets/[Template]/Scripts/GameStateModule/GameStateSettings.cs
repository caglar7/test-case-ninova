using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Template
{
    [CreateAssetMenu(menuName = "Template/Settings/GameStateSettings", fileName = "GameStateSettings", order = 0)]
    public class GameStateSettings : ScriptableObject
    {
        private const string Path = "GameSettings/GameStateSettings";

        private static GameStateSettings _instance;
        public static GameStateSettings Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = Resources.Load<GameStateSettings>(Path);

                    if(_instance == null)
                        Debug.LogError("not found in resources");
                }
                return _instance;
            }
        }

        [Header("Delays")] 
        public float delayLevelComplete = 1f;
        public float delayLevelFailed = 1f;
    }
}