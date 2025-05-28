using System.Collections.Generic;
using UnityEngine;

namespace Template.ObjectGridModule
{
    public class VertexGridSlot : GridSlot
    {
        [Header("Vertex Grid Slot")] 
        public List<BaseMono> currentObjects = new List<BaseMono>();
        
        
        public override void FillSlot(BaseMono obj)
        {
            currentObjects.Add(obj);
        }

        public override void ClearSlot()
        {
            if(currentObjects.Count > 0)
                currentObjects.RemoveAt(0);
        }
        
        public override bool IsFilled()
        {
            return currentObjects.Count > 0;
        }
        
        public override bool IsEmpty()
        {
            return currentObjects.Count == 0;
        }
    }
}