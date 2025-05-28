


using Template;
using UnityEngine;

public abstract class BaseInitModules : MonoBehaviour 
{
    [Header("Modules to Init")]
    public GameObject[] modules;

    public virtual void Init() 
    {
        for (int i = 0; i < modules.Length; i++)
        {
            if(modules[i].gameObject.activeInHierarchy && 
               modules[i].TryGetComponent(out IModuleInit iModuleInit))
            {
                iModuleInit.Init();
            }
        }
    }
}