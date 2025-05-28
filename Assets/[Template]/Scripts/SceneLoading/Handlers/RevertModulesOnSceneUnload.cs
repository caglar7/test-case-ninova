

using System.Collections.Generic;
using Template;
using UnityEngine;

public class RevertModulesOnSceneUnload : SceneLoadedHandler 
{
    [Header("Modules To Revert")]
    public List<Transform> modules = new List<Transform>();

    public override void HandleWhenAnyUnloaded()
    {
        base.HandleWhenAnyUnloaded();

        for (int i = 0; i < modules.Count; i++)
        {
            if(modules[i].TryGetComponent(out IModuleInitRevert iModuleRevert) == true)
            {
                iModuleRevert.InitRevert();
            }
        }
    }
}