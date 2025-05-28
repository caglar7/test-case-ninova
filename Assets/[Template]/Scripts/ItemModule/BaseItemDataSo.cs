using Sirenix.OdinInspector;
using Template;
using UnityEngine;

namespace _GAME.Scripts.ItemModule
{
    [CreateAssetMenu(menuName = "Template/New Base Item", fileName = "New Base Item")]
    public class BaseItemDataSo : ScriptableObject
    {
        [Header("Base Data")]
        [PreviewField]
        public Sprite icon;
        
        public string itemName;
        
        public PoolObject poolObject;
    }
}