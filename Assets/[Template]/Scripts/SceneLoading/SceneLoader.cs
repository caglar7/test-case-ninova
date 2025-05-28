

using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Template
{
    public class SceneLoader : Singleton<SceneLoader>, IModuleInit
    {
        [Space]
        [Header("Load Scenes At Start")]
        public SceneSo[] scenesToLoad;

        [Space]
        [Header("All Scenes")]
        public SceneSo[] allScenes;

        private List<SceneSo> _currentlyLoadedScenes = new List<SceneSo>();
        private SceneSo _cachedSceneSo;

        private void OnEnable() 
        {
            SceneManager.sceneLoaded += SceneLoaded;
            SceneManager.sceneUnloaded += SceneUnloaded;
        }

        private void OnDisable() 
        {
            SceneManager.sceneLoaded -= SceneLoaded;
            SceneManager.sceneUnloaded -= SceneUnloaded;
        }

        private void Start() 
        {
            Init();    
        }

        public void Init()
        {
            for (int i = 0; i < scenesToLoad.Length; i++)
            {
                LoadScene(scenesToLoad[i]);
            }
        }

        public void LoadScene(SceneSo sceneSo)
        {
            TryAddToCurrentlyLoaded(sceneSo);

            GeneralUtils.Delay(sceneSo.data.loadDelay, () =>
            {
                SceneManager.LoadSceneAsync(sceneSo.name, LoadSceneMode.Additive);
            });
        }



        public void LoadScene(SceneType sceneType)
        {
            _cachedSceneSo = GetSceneWithType(sceneType); 

            LoadScene(_cachedSceneSo);
        }

        public void UnLoadScene(SceneSo sceneSo)
        {
            if(_currentlyLoadedScenes.Contains(sceneSo) == false) return;

            GeneralUtils.Delay(sceneSo.data.unloadDelay, () =>
            {
                SceneManager.UnloadSceneAsync(sceneSo.name);
            });
        }

        public void UnLoadScene(SceneType sceneType)
        {
            _cachedSceneSo = GetSceneWithType(sceneType);

            UnLoadScene(_cachedSceneSo);
        }

        private void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            foreach (SceneSo sceneSo in allScenes)
            {
                if(sceneSo.name == scene.name)
                {
                    SceneLoaderEvent.SceneSoLoaded?.Invoke(sceneSo);
                    break;
                }
            }
        }

        private void SceneUnloaded(Scene scene)
        {
            foreach (SceneSo sceneSo in allScenes)
            {
                if(sceneSo.name == scene.name)
                {
                    SceneLoaderEvent.SceneSoUnloaded?.Invoke(sceneSo);
                    break;
                }
            }
        }

        private SceneSo GetSceneWithType(SceneType sceneType)
        {
            foreach (SceneSo sceneSo in allScenes)
            {
                if(sceneSo.data.type == sceneType) 
                {
                    return sceneSo;
                }
            }
            return null;
        }


        private void TryAddToCurrentlyLoaded(SceneSo sceneSo)
        {
            if (_currentlyLoadedScenes.Contains(sceneSo) == false)
                _currentlyLoadedScenes.Add(sceneSo);
        }
    }
}
