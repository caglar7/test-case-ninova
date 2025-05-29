using System.Collections.Generic;
using _GAME_.Scripts.StickmanModule;
using Template.ObjectGridModule;
using UnityEngine;

namespace _GAME_.Scripts.StickmanGridModule
{
    public class StickmanGrid : ObjectGrid
    {
        [Header("Stickman Grid")] 
        public GameObject stickmanPrefab;

        private List<Stickman> _stickmans = new();
        
        
        public void FillWithRandomStickmen()
        {
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    Stickman stickman = Instantiate(stickmanPrefab).GetComponent<Stickman>();

                    stickman.Init();

                    stickman.Transform.name = "Stickman " +  (_stickmans.Count + 1).ToString();
                    
                    FillSlot(stickman, row, column);
                    
                    _stickmans.Add(stickman);

                    stickman.Transform.position = GetSlot(row, column).objectHolder.position;
                }
            }
        }
        
        public override void FillSlot(BaseMono obj, int row, int column)
        {
            Slots[row, column].FillSlot(obj);
            obj.Transform.SetParent(objectHolder);
        }

        public void ClearSlotWith(Stickman stickman)
        {
            foreach (GridSlot slot in Slots)
            {
                if (slot.IsFilled() &&
                    slot.currentObject is Stickman check &&
                    check == stickman)
                {
                    slot.ClearSlot();
                }
            }
        }
    }
}