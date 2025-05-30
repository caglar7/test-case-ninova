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

        private int _agentCount;
        private int _stickmanCount;

        public void Init()
        {
            InitStages();

            FillFirstStageWithStickmen();

            StageEvents.OnStageCompleted += HandleStageCompleted;
        }
        private void OnDisable()
        {
            StageEvents.OnStageCompleted -= HandleStageCompleted;
        }

        private void HandleStageCompleted(Stage obj)
        {
        }

        public void MoveToNextStage()
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
                        
                    MoveToNextStageSlots(stickman, bridgeClosest, i);
                }
            }

            _stickmanCount = 0;
            _agentCount = 0;
            for (int col = 0; col < 5; col++)
            {
                for (int row = 0; row < 5; row++)
                {
                    if (CurrentStage.stickmanGrid.GetSlot(row, col).IsFilled() &&
                        CurrentStage.stickmanGrid.GetSlot(row, col).currentObject is Stickman stickman)
                    {
                        _stickmanCount++;
                    }
                }
            }   
            for (int col = 0; col < 5; col++)
            {
                for (int row = 0; row < 5; row++)
                {
                    if (CurrentStage.stickmanGrid.GetSlot(row, col).IsFilled() &&
                        CurrentStage.stickmanGrid.GetSlot(row, col).currentObject is Stickman stickman)
                    {
                        stickman.AgentMode(() =>
                        {
                            CheckAgentCount(Step2);
                        });
                    }
                }
            }  
        }
        
        private void InitStages()
        {
            foreach (Stage stage in stages)
            {
                stage.Init();
            }
        }
        
        private void Step2()
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
            
            BaseComponentFinder.instance.CameraManager
                .ChangeAngle(currentStageIndex+1, duration);
            
            StartCoroutine(SetStageIndexAfter(duration));
        }

        private void CheckAgentCount(Action onAllAgent)
        {
            _agentCount++;
            if (_agentCount == _stickmanCount)
            {
                onAllAgent?.Invoke();
            }
        }

        IEnumerator SetStageIndexAfter(float f)
        {
            yield return new WaitForSeconds(f);
            currentStageIndex++;
        }

        private void MoveToNextStageGrid(Stickman stickman, int row, int col)
        {
            CurrentStage.stickmanGrid.GetSlot(row, col).ClearSlot();

            NextStage.stickmanGrid.GetSlot(row, col).FillSlot(stickman);
                        
            Bridge bridgeClosest = GeneralMethods.FindClosest(CurrentStage.bridgeHandler.bridges, 
                stickman.Transform.position);
            
            stickman.AgentMode(() =>
            {
                stickman.CrossTheBridge(bridgeClosest, () =>
                {
                    stickman.moverPoint.Move(NextStage.stickmanGrid.GetSlot(row, col).objectHolder.position);
                    StartCoroutine(SetObstacleModeAfter(stickman, 1f));
                });
            });
        }

        IEnumerator SetObstacleModeAfter(Stickman stickman, float delay)
        {
            yield return new WaitForSeconds(delay);
            stickman.ObstacleMode();
        }
        
        private void MoveToNextStageSlots(Stickman stickman, Bridge bridgeClosest, int slotIndex)
        {
            stickman.timer.PauseTimer();
            
            stickman.CrossTheBridge(bridgeClosest, () =>
            {
                stickman.moverPoint.Move(NextStage.stickmanSlotHandler.slots[slotIndex].objectHolder.position);
                stickman.moverPoint.onDestinationReachedOnce = () =>
                {
                    stickman.timer.ResumeTimer();
                };
            });
        }

        private void FillFirstStageWithStickmen()
        {
            stages[0].stickmanGrid.FillWithRandomStickmen();
        }

 
    }
}