

using System.Collections.Generic;
using Template;
using UnityEngine;

public class InitModulesOnSceneLoaded : SceneLoadedHandler 
{
    [Header("Modules To Init")]
    public List<Transform> modules = new List<Transform>();

    public override void HandleWhenAnyLoaded()
    {
        base.HandleWhenAnyLoaded();

        for (int i = 0; i < modules.Count; i++)
        {
            if(modules[i].TryGetComponent(out IModuleInit iModuleInit) == true)
            {
                iModuleInit.Init();
            }
        }
    }
}