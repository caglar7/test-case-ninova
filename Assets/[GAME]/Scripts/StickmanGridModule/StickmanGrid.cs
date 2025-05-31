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

        [Header("Edit Spawn Data")] 
        public List<StickmanSpawnData> SpawnDatas = new();
        
        
        public void FillWithRandomStickmen()
        {
            int row = 0;
            int column = 0;

            for (int i = 0; i < SpawnDatas.Count; i++)
            {
                row = i / 5;
                column = i % 5;
                
                Stickman stickman = Instantiate(stickmanPrefab).GetComponent<Stickman>();

                stickman.colorComponent.SetColor(SpawnDatas[i].ColorType);
                
                stickman.Init();
                
                stickman.AddBricks(SpawnDatas[i].ColorType, SpawnDatas[i].BrickCount);
                
                stickman.Transform.position = GetSlot(row, column).objectHolder.position;

                stickman.Transform.name = "Stickman " + (i + 1);

                FillSlot(stickman, row, column);
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