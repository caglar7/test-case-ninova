using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Template
{
    public class SlotHandler : BaseMono, IModuleInit
    {
        [Header("Base")]
        public List<Slot> slots = new List<Slot>();
        
        public virtual void Init()
        {
            
        }
        public virtual void OnDisable()
        {
            
        }

        
        IEnumerator ClearSlotCo(Slot slot)
        {
            yield return new WaitForSeconds(1f);
            slot.ClearSlot();
        }

        public bool TryGetEmptySlot(out Slot slotFound)
        {
            slotFound = null;

            for (int i=0; i < slots.Count; i++)
            {
                if (slots[i].currentObject == null)
                {
                    slotFound = slots[i];
                    return true;
                }
            }

            return false;
        }

        public bool TryGetEmptySlotFromRight(out Slot slotFound)
        {
            slotFound = null;

            int indexObjectOnRight = -1;
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].currentObject != null)
                {
                    indexObjectOnRight = i;
                }
            }

            if (indexObjectOnRight == (slots.Count - 1))
                return false;
            
            slotFound = slots[indexObjectOnRight + 1];
            return true;
        }
        
        public bool TryGetEmptySlotsFromRight(int count, out List<Slot> slotsOnRight)
        {
            slotsOnRight = new List<Slot>();

            int indexObjectOnRight = -1;
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].currentObject != null)
                {
                    indexObjectOnRight = i;
                }
            }

            if (indexObjectOnRight == (slots.Count - 1))
                return false;

            for (int i = 1; i <= count; i++)
            {
                if ((indexObjectOnRight + i) < slots.Count)
                {
                    slotsOnRight.Add(slots[indexObjectOnRight + i]);
                }
                else return false;
            }
            return true;
        }
        
        

        public bool TryGetSlotWithObject(BaseMono obj, out Slot slotFound)
        {
            slotFound = null;

            for (int i=0; i < slots.Count; i++)
            {
                if (slots[i].currentObject == obj)
                {
                    slotFound = slots[i];
                    return true;
                }
            }

            return false;
        }

        public bool AreSlotsEmpty()
        {
            foreach (Slot slot in slots)
            {
                if (slot.currentObject is not null)
                    return false;
            }
            return true;
        }

        public bool AreSlotsFull()
        {
            foreach (Slot slot in slots)
            {
                if (slot.currentObject is null)
                    return false;
            }
            return true;
        }

        public bool IsThereObject<T>(out T objectFound) where T : BaseMono
        {
            objectFound = null;

            foreach (Slot slot in slots)
            {
                if (slot.currentObject is T obj)
                {
                    objectFound = obj;
                    return true;
                }
            }

            return false;
        }

        public int GetSlotIndex(BaseMono obj)
        {
            int index = 0;
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].currentObject == obj)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }
    }
}