

using UnityEngine;

namespace _GAME.Scripts.ItemModule
{
    public abstract class BaseItem : BaseMono
    {
        [Header("Base")] 
        public BaseItemDataSo dataSo;
        
        public abstract void Init();
    }
}