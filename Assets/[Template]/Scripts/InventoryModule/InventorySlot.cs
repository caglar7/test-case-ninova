using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  currently, lets just assume there is only one stack of every item
/// </summary>

namespace Template
{
    [Serializable]
    public class InventorySlot
    {
        public BaseMono _obj;
        public BaseMono Obj => _obj;
    

        public InventorySlot()
        {

        }    
    
        public InventorySlot(BaseMono item)
        {
            AddItem(item);
        }


        public void AddItem(BaseMono item)
        {
            _obj = item;
        }

        public bool IsSlotEmpty()
        {
            return _obj == null;
        }

        public void ClearSlot()
        {
            _obj = null;
        }
    }
}
