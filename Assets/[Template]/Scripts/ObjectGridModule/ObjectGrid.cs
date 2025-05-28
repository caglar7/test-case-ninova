using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Template.ObjectGridModule
{
    public class ObjectGrid : BaseMono, IModuleInit
    {
        [Header("References")]
        public GameObject prefabGridSlot; // later from pool
        public Transform slotHolder;
        public Transform objectHolder;
        
        [Header("Grid Settings")]
        public int rowCount = 5;
        public int columnCount = 5;
        public float slotScale = 1f;
        
        [Header("Grid Unit Settings")]
        public float unitDistanceX = 1f;
        public float unitDistanceY = 1f;
        
        private Vector3[,] _localPositions;
        
        
        private GridSlot[,] _slots;
        public GridSlot[,] Slots => _slots;

        
        private GridSlot _slotSpawned;
        
        
        
        public void Init()
        {
            CalculateLocalPositions(rowCount, columnCount, out _localPositions);

            SpawnSlots();
        }

        private void CalculateLocalPositions(int row, int column, out Vector3[,] localPositions)
        {
            localPositions = new Vector3[row,column];
            
            Vector3 topLeftPos = Vector3.zero;
            topLeftPos.x = -1 * (row - 1) * 0.5f * unitDistanceX;
            topLeftPos.y = 1 * (column - 1) * 0.5f * unitDistanceY;
            Vector3 nextLocalPos = topLeftPos;
            
            for (int i = 0; i < column; i++)
            {
                nextLocalPos.x = topLeftPos.x;
                
                for (int j = 0; j < row; j++)
                {
                    localPositions[i, j] = nextLocalPos;
                    
                    nextLocalPos.x += unitDistanceX;
                }
                nextLocalPos.y -= unitDistanceY;
            }
        }
        
        private void SpawnSlots()
        {
            _slots = new GridSlot[rowCount, columnCount];
            
            GridIteration(rowCount, columnCount, (row ,column ) =>
            {
                _slotSpawned = Instantiate(prefabGridSlot, slotHolder).GetComponent<GridSlot>();
            
                _slotSpawned.Transform.localPosition  = _localPositions[row, column];
            
                _slotSpawned.Transform.localScale = Vector3.one * slotScale;

                _slotSpawned.rowIndex = row;
            
                _slotSpawned.columnIndex = column;

                _slots[row, column] = _slotSpawned;
            });
        }
        
        
        public virtual void FillSlot(BaseMono obj, int index)
        {
            FillSlot(obj, index / rowCount, index % columnCount);
        }
        public virtual void FillSlot(BaseMono obj, int row, int column)
        {
            _slots[row, column].FillSlot(obj);
            obj.Transform.SetParent(objectHolder);
            obj.Transform.position = _slots[row, column].Transform.position;
        }
        public virtual void FillSlots(BaseMono obj, List<GridSlot> slots)
        {
            foreach (GridSlot slot in slots)
            {
                slot.FillSlot(obj);
            }
        }
        public void SetTransform(BaseMono obj, List<GridSlot> slots)
        {
            obj.Transform.position = PositionUtility.GetAveragePosition(slots);
            
            obj.Transform.SetParent(objectHolder);
        }
        public void ClearSlots(List<GridSlot> slots)
        {
            foreach (GridSlot slot in slots)
            {
                slot.ClearSlot();
            }
        }
        public bool IsRangeFilled(int rowStart, int columnStart, int range)
        {
            bool isFilled = false;
            
            for (int rowCounter = rowStart; rowCounter < (rowStart+range); rowCounter++)
            {
                for (int columnCounter = columnStart; columnCounter < (columnStart+range); columnCounter++)
                {
                    if (_slots[rowCounter, columnCounter].IsFilled())
                    {
                        isFilled = true;
                        break;
                    }
                }
            }
            return isFilled;
        }
        
        
        public GridSlot GetSlot(int index)
        {
            return _slots[index / rowCount, index % columnCount];
        }
        public GridSlot GetSlot(int row, int column)
        {
            return _slots[row, column];
        }
        public List<GridSlot> GetSlotRange(int rowStart, int columnStart, int range)
        {
            List<GridSlot> slots = new List<GridSlot>();
            for (int rowCounter = rowStart; rowCounter < (rowStart+range); rowCounter++)
            {
                for (int columnCounter = columnStart; columnCounter < (columnStart+range); columnCounter++)
                {
                    slots.Add(_slots[rowCounter, columnCounter]);
                }
            }
            return slots;
        }
        public int GetGridDistance(GridSlot slot1, GridSlot slot2)
        {
            return (Mathf.Abs(slot1.rowIndex - slot2.rowIndex)) + Mathf.Abs(slot1.columnIndex - slot2.columnIndex);
        }
        public float GetRealDistance(GridSlot slot1, GridSlot slot2)
        {
            return Vector3.Distance(slot1.Transform.position, slot2.Transform.position);
        }
        public List<GridSlot> GetDiagonalNeighbors(int rowCenter, int columnCenter)
        {
            List<GridSlot> slots = new List<GridSlot>();
            slots.Add(_slots[rowCenter+1, columnCenter+1]);
            slots.Add(_slots[rowCenter-1, columnCenter-1]);
            slots.Add(_slots[rowCenter+1, columnCenter-1]);
            slots.Add(_slots[rowCenter-1, columnCenter+1]);
            return slots;
        }


        public bool AreIndexesInRange(int rowIndex, int columnIndex)
        {
            return IsRowInRange(rowIndex) && IsColumnInRange(columnIndex);
        }
        public bool IsRowInRange(int rowIndex)
        {
            return (rowIndex >= 0 && rowIndex <= (rowCount - 1));
        }        
        public bool IsColumnInRange(int columnIndex)
        {
            return (columnIndex >= 0 && columnIndex <= (columnCount - 1));
        }

        
        
        private void GridIteration(int rowRange, int columnRange, Action<int, int> onIteration)
        {
            for (int i = 0; i < columnRange; i++)
            {
                for (int j = 0; j < rowRange; j++)
                {
                    onIteration?.Invoke(i, j);
                }
            }
        }
    }
}