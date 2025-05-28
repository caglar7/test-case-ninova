

using System;

namespace Template
{
    public static class SceneLoaderEvent
    {
        public static Action<SceneSo> SceneSoLoaded;    
        public static Action<SceneSo> SceneSoUnloaded;    
    }
}