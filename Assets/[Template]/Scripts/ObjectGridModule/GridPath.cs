using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Template.ObjectGridModule
{
    public class GridPath : BaseMono
    {
        public ObjectGrid grid;
        
        public bool TryGetPathBetween(GridSlot start, GridSlot target, out List<Vector3> points)
        {
            bool didGetPath = false;
            
            points = new List<Vector3>();
            
            List<GridSlot> intersectionSlots = new List<GridSlot>();

            intersectionSlots = GetIntersectionSlots(GetWaveSlots(start), GetWaveSlots(target));
            
            if (AreNeighbors(start, target))
            {
                points.Add(start.Transform.position);
                points.Add(target.Transform.position);
                didGetPath = true;
            }
            else if (intersectionSlots.Count > 0 && AreDiagonalNeighbors(start, target))
            {
                points.Add(start.Transform.position);
                points.Add(intersectionSlots[0].Transform.position);
                points.Add(target.Transform.position);
                didGetPath = true;
            }
            else if (intersectionSlots.Count > 0 && AreSlotsInSameLine(start, target))
            {
                points.Add(start.Transform.position);
                points.Add(target.Transform.position);
                didGetPath = true;
            }
            else if (intersectionSlots.Count > 0 && AreSlotsInSameLine(start, target) == false)
            {
                points.Add(start.Transform.position);
                points.Add(intersectionSlots[0].Transform.position);
                points.Add(target.Transform.position);
                didGetPath = true;
            }
            else if(intersectionSlots.Count == 0)
            {
                foreach (GridSlot slot in GetWaveSlots(start))
                {
                    List<GridSlot> unitIntersectionSlot = GetIntersectionSlots(GetWaveSlots(slot), GetWaveSlots(target));
                    if (unitIntersectionSlot.Count > 0)
                    {
                        points.Add(start.Transform.position);
                        points.Add(slot.Transform.position);
                        points.Add(unitIntersectionSlot[0].Transform.position);
                        points.Add(target.Transform.position);
                        didGetPath = true;
                        break;
                    }
                }
            }

            return didGetPath;
        }
        
        
        

        private List<GridSlot> GetIntersectionSlots(List<GridSlot> slots1, List<GridSlot> slots2)
        {
            List<GridSlot> intersectionSlots = new List<GridSlot>();

            foreach (GridSlot gridSlot in slots1)
            {
                if (slots2.Contains(gridSlot))
                {
                    intersectionSlots.Add(gridSlot);
                }
            }
            return intersectionSlots;
        }
        
        private List<GridSlot> GetWaveSlots(GridSlot slotCenter)
        {
            List<GridSlot> waveSlots = new List<GridSlot>();

            GridSlot slotChecked = null;
            
            AddSlotsOnRightUntilBlock(slotCenter, waveSlots);

            AddSlotsOnLeftUntilBlock(slotCenter, waveSlots);

            AddSlotsOnUpUntilBlock(slotCenter, waveSlots);

            AddSlotOnDownUntilBlock(slotCenter, waveSlots);

            return waveSlots;
        }

        private bool AreNeighbors(GridSlot slot1, GridSlot slot2)
        {
            int rowDistance = Mathf.Abs(slot1.rowIndex - slot2.rowIndex);
            
            int columnDistance = Mathf.Abs(slot1.columnIndex - slot2.columnIndex);
            
            bool result = (rowDistance + columnDistance == 1);
            
            return result;
        }

        private bool AreDiagonalNeighbors(GridSlot slot1, GridSlot slot2)
        {
            int rowDistance = Mathf.Abs(slot1.rowIndex - slot2.rowIndex);
            
            int columnDistance = Mathf.Abs(slot1.columnIndex - slot2.columnIndex);
            
            bool result = rowDistance == 1 && columnDistance == 1;
            
            return result;
        }
        
        private bool AreSlotsInSameLine(GridSlot slot1, GridSlot slot2)
        {
            int rowDistance = Mathf.Abs(slot1.rowIndex - slot2.rowIndex);
            
            int columnDistance = Mathf.Abs(slot1.columnIndex - slot2.columnIndex);
                
            bool result = (rowDistance == 0 || columnDistance == 0);
            
            return result;
        }
        
        

        private void AddSlotOnDownUntilBlock(GridSlot slotCenter, List<GridSlot> wave)
        {
            GridSlot slotChecked;
            int rowIndexToDown = slotCenter.rowIndex + 1;
            if (grid.IsRowInRange(rowIndexToDown))
            {
                for (int i = rowIndexToDown; i < grid.rowCount; i++)
                {
                    slotChecked = grid.Slots[i, slotCenter.columnIndex];

                    if (slotChecked.IsFilled() == false)
                        wave.Add(slotChecked);
                    else
                        break;
                }
            }
        }

        private void AddSlotsOnUpUntilBlock(GridSlot slotCenter, List<GridSlot> wave)
        {
            GridSlot slotChecked;
            int rowIndexToUp = slotCenter.rowIndex - 1;
            if (grid.IsRowInRange(rowIndexToUp))
            {
                for (int i = rowIndexToUp; i >= 0; i--)
                {
                    slotChecked = grid.Slots[i, slotCenter.columnIndex];

                    if (slotChecked.IsFilled() == false)
                        wave.Add(slotChecked);
                    else
                        break;
                }
            }
        }

        private void AddSlotsOnLeftUntilBlock(GridSlot slotCenter, List<GridSlot> wave)
        {
            GridSlot slotChecked;
            int columnIndexToLeft = slotCenter.columnIndex - 1;
            if (grid.IsColumnInRange(columnIndexToLeft))
            {
                for (int i = columnIndexToLeft; i >= 0; i--)
                {
                    slotChecked = grid.Slots[slotCenter.rowIndex, i];

                    if (slotChecked.IsFilled() == false)
                        wave.Add(slotChecked);
                    else
                        break;
                }
            }
        }

        private void AddSlotsOnRightUntilBlock(GridSlot slotCenter, List<GridSlot> wave)
        {
            GridSlot slotChecked;
            int columnIndexToRight = slotCenter.columnIndex + 1;
            if (grid.IsColumnInRange(columnIndexToRight))
            {
                for (int i = columnIndexToRight; i < grid.columnCount; i++)
                {
                    slotChecked = grid.Slots[slotCenter.rowIndex, i];

                    if (slotChecked.IsFilled() == false)
                        wave.Add(slotChecked);
                    else
                        break;
                }
            }
        }
        
        private void TestAnimateSlot(GridSlot gridSlot)
        {
            TweenAnimation.PunchScalePreset(gridSlot.gfxMain);

            Color color = gridSlot.gfxMain.GetComponentInChildren<SpriteRenderer>().color;
            gridSlot.gfxMain.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            GeneralUtils.Delay(0.3f, () =>
            {
                gridSlot.gfxMain.GetComponentInChildren<SpriteRenderer>().color = color;
            });
        }
    }
}