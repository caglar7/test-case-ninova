

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Template
{
    public class SceneLoadedHandler : MonoBehaviour 
    {
        [Header("Settings")]
        public SceneLoadedHandlerType handlerType;
        public float delay = 0f;

        /// <summary>
        /// when any one of these scenes are loaded, do something
        /// </summary>
        [Header("Scenes")]
        public List<SceneSo> scenes = new List<SceneSo>();

        private void OnEnable() 
        {
            SceneLoaderEvent.SceneSoLoaded += LoadedAll;
            SceneLoaderEvent.SceneSoLoaded += LoadedAny;
            SceneLoaderEvent.SceneSoUnloaded += UnloadedAny;
        }

        private void OnDisable() 
        {
            SceneLoaderEvent.SceneSoLoaded -= LoadedAll;
            SceneLoaderEvent.SceneSoLoaded -= LoadedAny;
            SceneLoaderEvent.SceneSoUnloaded -= UnloadedAny;
        }

        public virtual void HandleWhenAllLoaded()
        {
            
        }

        public virtual void HandleWhenAnyLoaded()
        {

        }

        public virtual void HandleWhenAnyUnloaded()
        {

        }

        private void LoadedAll(SceneSo sceneSo)
        {     
            if(handlerType != SceneLoadedHandlerType.LoadedAll) return;

            if(scenes.Contains(sceneSo))
            {
                scenes.Remove(sceneSo);

                if(scenes.Count == 0)
                {
                    StartCoroutine(GeneralUtils.DelayCo(delay, HandleWhenAllLoaded));
                }
            }
        }

        private void LoadedAny(SceneSo sceneSo)
        {       
            if(handlerType != SceneLoadedHandlerType.LoadedAny) return;

            if(scenes.Contains(sceneSo))
            {                
                StartCoroutine(GeneralUtils.DelayCo(delay, HandleWhenAnyLoaded));
            }
        }

        private void UnloadedAny(SceneSo sceneSo)
        {
            if(handlerType != SceneLoadedHandlerType.UnloadedAny) return;

            if(scenes.Contains(sceneSo))
            {                
                StartCoroutine(GeneralUtils.DelayCo(delay, HandleWhenAnyUnloaded));
            }
        }
    }
}