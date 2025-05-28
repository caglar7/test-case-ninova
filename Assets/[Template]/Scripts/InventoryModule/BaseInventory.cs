

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Template;
using UnityEngine;

namespace Template
{
    public class BaseInventory : BaseMono, IModuleInit
    {
        [Header("References")] 
        public Transform itemHolder;
        
        [Header("Settings")]
        public int slotCount = 9;

        public Action<InventorySlot, int> OnInventorySlotUpdated;
        public Action<BaseMono> OnItemAdded;

        public List<InventorySlot> inventorySlots;

        private List<BaseMono> _itemList = new List<BaseMono>();
        public List<BaseMono> ItemList => _itemList;


        public virtual void Init()
        {
            _itemList = new List<BaseMono>();
            
            inventorySlots = new List<InventorySlot>();

            for (int i = 0; i < slotCount; i++)
            {
                inventorySlots.Add(new InventorySlot());
            }
        }

        public bool TryAddItem(BaseMono item)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventorySlots[i].IsSlotEmpty() == true)
                {
                    inventorySlots[i].AddItem(item);

                    item.transform.SetParent(itemHolder);

                    HandleItemAdded(item);

                    _itemList.Add(item);

                    OnInventorySlotUpdated?.Invoke(inventorySlots[i], i);

                    OnItemAdded?.Invoke(item);

                    return true;
                }
            }

            Debug.Log("Couldnt find empty slot");
            return false;
        }

        public bool TryRemoveItem(BaseMono item)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventorySlots[i].Obj == item)
                {
                    inventorySlots[i].ClearSlot();

                    HandleItemRemoved(item);

                    _itemList.Remove(item);

                    OnInventorySlotUpdated?.Invoke(inventorySlots[i], i);

                    return true;
                }
            }

            Debug.Log("No such item found to remove");
            return false;
        }

        // public bool ContainsItem(BaseItemData checkData, out BaseMono item)
        // {
        //     item = null;
        //
        //     foreach (InventorySlot slot in inventorySlots)
        //     {
        //         if (slot.Item != null && slot.Item.itemData == checkData)
        //         {
        //             item = slot.Item;
        //             return true;
        //         }
        //     }
        //
        //     Debug.Log("No such item (" + checkData.itemName + ") found");
        //     return false;
        // }

        public BaseMono FindWithType<T>() where T : BaseMono
        {
            foreach (var baseItem in _itemList)
            {
                if (baseItem is T foundItem)
                {
                    return foundItem;
                }
            }

            return null;
        }

        public bool IsThereEmptySlot()
        {
            bool isThere = false;

            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventorySlots[i].IsSlotEmpty() == true)
                {
                    isThere = true;
                    break;
                }
            }

            return isThere;
        }

        public Vector3 TargetPosition()
        {
            return itemHolder.position + TargetLocalPosition();
        }
        
        
        public virtual void HandleItemAdded(BaseMono item)
        {
            item.gameObject.SetActive(false);
        }

        public virtual void HandleItemRemoved(BaseMono item)
        {
            item.transform.SetParent(null);
        }
        public virtual Vector3 TargetLocalPosition()
        {
            return Vector3.zero;
        }
    }
}