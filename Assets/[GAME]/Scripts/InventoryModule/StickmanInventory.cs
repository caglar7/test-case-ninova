using Template;
using UnityEngine;

namespace _GAME_.Scripts.InventoryModule
{
    public class StickmanInventory : BaseInventory
    {
        [Header("Stickman Inventory")]
        public float offsetY = 0.5f;

        public override void HandleItemAdded(BaseMono item)
        {
            item.Transform.SetParent(itemHolder);
            item.Transform.localPosition = new Vector3(0f, (itemHolder.childCount -1) * offsetY, 0f);
        }
    }
}