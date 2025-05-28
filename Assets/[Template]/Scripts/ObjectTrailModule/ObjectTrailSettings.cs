using _GAME.Scripts.ItemTransferModule;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Template
{
    [CreateAssetMenu(menuName = "Template/Settings/ObjectTrailSettings", fileName = "ObjectTrailSettings", order = 0)]

    public class ObjectTrailSettings : ScriptableObject
    {
        private const string Path = "GameSettings/ObjectTrailSettings";

        private static ObjectTrailSettings _instance;

        public static ObjectTrailSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<ObjectTrailSettings>(Path);

                    if (_instance == null)
                        Debug.LogError("not found in resources");
                }

                return _instance;
            }
        }

        [Header("Timer")] 
        public float timerPeriod = .1f;
    }
}