using System;
using System.Collections;
using System.Collections.Generic;
using _GAME_.Scripts.BridgeModule;
using _GAME_.Scripts.StickmanModule;
using Sirenix.OdinInspector;
using Template;
using Unity.VisualScripting;
using UnityEngine;

namespace _GAME_.Scripts.StageModule
{
    public class StageHandler : BaseMono, IModuleInit
    {
        public List<Stage> stages = new();

        public int currentStageIndex = 0;
        
        public Stage CurrentStage => stages[currentStageIndex];
        public Stage NextStage => stages[currentStageIndex + 1];

        public float duration = 1f;

        private int _currentStickmanCount;
        
        private int _targetStickmanCount;

        public void Init()
        {
            InitStages();

            FillFirstStageWithStickmen();

            StageEvents.OnStageCompleted += MoveToNextStage;
        }
        private void OnDisable()
        {
            StageEvents.OnStageCompleted -= MoveToNextStage;
        }
        
        
        
        
        private void InitStages()
        {
            foreach (Stage stage in stages)
            {
                stage.Init();
            }
        }
        private void FillFirstStageWithStickmen()
        {
            stages[0].stickmanGrid.FillWithRandomStickmen();
        }

        
        
        
        private void MoveToNextStage()
        {
            GetStickmanCounts();

            MoveStickmansFromSlots();
            
            MoveStickmansFromGrid();  
            
            MoveCamera();
        }
        private void GetStickmanCounts()
        {
            _targetStickmanCount = 0;
            _currentStickmanCount = 0;
            for (int i = 0; i < CurrentStage.stickmanSlotHandler.slots.Count; i++)
            {
                if (CurrentStage.stickmanSlotHandler.slots[i].IsFilled())
                {
                    _targetStickmanCount++;
                }
            }
            for (int col = 0; col < 5; col++)
            {
                for (int row = 0; row < 5; row++)
                {
                    if (CurrentStage.stickmanGrid.GetSlot(row, col).IsFilled() &&
                        CurrentStage.stickmanGrid.GetSlot(row, col).currentObject is Stickman stickman)
                    {
                        _targetStickmanCount++;
                    }
                }
            }
        }
        private void MoveStickmansFromSlots()
        {
            for (int i = 0; i < CurrentStage.stickmanSlotHandler.slots.Count; i++)
            {
                if (CurrentStage.stickmanSlotHandler.slots[i].IsFilled() &&
                    CurrentStage.stickmanSlotHandler.slots[i].currentObject is Stickman stickman)
                {
                    CurrentStage.stickmanSlotHandler.slots[i].ClearSlot();
                    
                    NextStage.stickmanSlotHandler.slots[i].FillSlot(stickman);

                    Bridge bridgeClosest = GeneralMethods.FindClosest(CurrentStage.bridgeHandler.bridges, 
                        stickman.Transform.position);
                    MoveToNextStageSlot(stickman, bridgeClosest, i);
                }
            }
        }
        private void MoveStickmansFromGrid()
        {
            for (int col = 0; col < 5; col++)
            {
                for (int row = 0; row < 5; row++)
                {
                    if (CurrentStage.stickmanGrid.GetSlot(row, col).IsFilled() &&
                        CurrentStage.stickmanGrid.GetSlot(row, col).currentObject is Stickman stickman)
                    {
                        MoveToNextStageGrid(stickman, row, col);
                    }
                }
            }
        }
        private void MoveCamera()
        {
            BaseComponentFinder.instance.CameraManager.ChangeAngle(currentStageIndex+1, duration);
        }




        
        
        private void MoveToNextStageSlot(Stickman stickman, Bridge bridgeClosest, int slotIndex)
        {
            stickman.timer.PauseTimer();
            
            stickman.CrossTheBridge(bridgeClosest, () =>
            {
                stickman.moverPoint.Move(NextStage.stickmanSlotHandler.slots[slotIndex].objectHolder.position);
                stickman.moverPoint.onDestinationReachedOnce = () =>
                {
                    CheckStickmanCount();
                    stickman.timer.ResumeTimer();
                };
            });
        }
        private void MoveToNextStageGrid(Stickman stickman, int row, int col)
        {
            stickman.AgentMode(() =>
            {
                CurrentStage.stickmanGrid.GetSlot(row, col).ClearSlot();

                NextStage.stickmanGrid.GetSlot(row, col).FillSlot(stickman);
                        
                Bridge bridgeClosest = GeneralMethods.FindClosest(CurrentStage.bridgeHandler.bridges, 
                    stickman.Transform.position);
            
                stickman.CrossTheBridge(bridgeClosest, () =>
                {
                    stickman.moverPoint.Move(NextStage.stickmanGrid.GetSlot(row, col).objectHolder.position);

                    stickman.moverPoint.onDestinationReachedOnce = CheckStickmanCount;
                });
            });
        }
        private void CheckStickmanCount()
        {
            _currentStickmanCount++;
            if (_currentStickmanCount == _targetStickmanCount)
            {
                SetToNextStage();
            }
        }
        private void SetToNextStage()
        {
            currentStageIndex++;
        }
    }
}