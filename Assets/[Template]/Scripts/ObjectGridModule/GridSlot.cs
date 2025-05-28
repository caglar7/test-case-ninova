using UnityEngine;

namespace Template.ObjectGridModule
{
    public class GridSlot : Slot
    {
        [Header("References")] 
        public Transform gfxMain;
        
        [Header("Coordinate Indexes")] 
        public int rowIndex;
        public int columnIndex;

        public override void FillSlot(BaseMono obj)
        {
            currentObject = obj;
        }
    }
}