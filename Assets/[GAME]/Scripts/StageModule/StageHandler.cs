using System;
using System.Collections;
using System.Collections.Generic;
using _GAME_.Scripts.BridgeModule;
using _GAME_.Scripts.ComponentAccess;
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
        
        public float duration = 1f;

        public float durationFinish = 1f;
        
        public Stage CurrentStage => stages[_currentStageIndex];
        public Stage NextStage => stages[_currentStageIndex + 1];


        private int _currentStickmanCount;
        
        private int _targetStickmanCount;
        
        private List<Stickman> _stickmansRemaining = new();
        
        private int _currentStageIndex;
        
        
        public void Init()
        {
            _currentStageIndex = 0;
            
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

        
        
        
        private void MoveToNextStage(Stage stage)
        {
            if (stage.isFinalStage)
            {
                FinishLevel();
                return;
            }
            
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
                        stickman.input.isInputActive = false;
                        
                        MoveToNextStageGrid(stickman, row, col);
                    }
                }
            }
        }
        private void MoveCamera()
        {
            BaseComponentFinder.instance.CameraManager.ChangeAngle(
                NextStage.cameraUnit, 
                duration
            );
        }

        private void FinishLevel()
        {
            GetRemainingStickmans();
            
            MoveStickmansToFinishLine();

            MoveCameraToFinish();
        }

        private void GetRemainingStickmans()
        {
            _stickmansRemaining.Clear();
            for (int i = 0; i < CurrentStage.stickmanSlotHandler.slots.Count; i++)
            {
                if (CurrentStage.stickmanSlotHandler.slots[i].IsFilled())
                {
                    if (CurrentStage.stickmanSlotHandler.slots[i].currentObject is Stickman stickman)
                    {
                        _stickmansRemaining.Add(stickman);
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
                        _stickmansRemaining.Add(stickman);
                    }
                }
            }
        }
        private void MoveStickmansToFinishLine()
        {
            for (int i = 0; i < ComponentFinder.instance.FinishHandler.finishSlotHandler.slots.Count; i++)
            {
                if (i < _stickmansRemaining.Count)
                {
                    MoveStickmanToFinishSlot(
                        _stickmansRemaining[i], 
                        i, 
                        (i == _stickmansRemaining.Count-1) ? HandleLastReached : null
                    );
                }
            }
        }
        private void MoveStickmanToFinishSlot(Stickman stickman, int slotIndex, Action onMoveDone = null)
        {
            ComponentFinder.instance.FinishHandler.finishSlotHandler.slots[slotIndex]
                .FillSlot(stickman);
            
            Bridge bridgeClosest = GeneralMethods.FindClosest(CurrentStage.bridgeHandler.bridges, 
                stickman.Transform.position);
            
            stickman.SetStickmanState(StickmanState.CarryRunning);
            
            stickman.CrossTheBridge(bridgeClosest, () =>
            {
                stickman.moverPoint.Move(
                    ComponentFinder.instance.FinishHandler.finishSlotHandler.slots[slotIndex].Transform.position
                );

                stickman.moverPoint.onDestinationReachedOnce = () =>
                {
                    stickman.SetStickmanState(StickmanState.CarryIdle);
                    
                    onMoveDone?.Invoke();
                };
            });
        }
        private void HandleLastReached()
        {
            StageEvents.OnAllStagesDone?.Invoke();
        }
        private void MoveCameraToFinish()
        {
            BaseComponentFinder.instance.CameraManager.ChangeAngle(
                ComponentFinder.instance.FinishHandler.cameraUnit, 
                durationFinish
            );
        }


        private void MoveToNextStageSlot(Stickman stickman, Bridge bridgeClosest, int slotIndex)
        {
            stickman.SetStickmanState(StickmanState.CarryRunning);
            
            stickman.timer.PauseTimer();
            
            stickman.CrossTheBridge(bridgeClosest, () =>
            {
                stickman.moverPoint.Move(NextStage.stickmanSlotHandler.slots[slotIndex].objectHolder.position);
                
                stickman.moverPoint.onDestinationReachedOnce = () =>
                {
                    stickman.SetStickmanState(StickmanState.CarryIdle);
                    
                    CheckStickmanCount();
                    
                    stickman.timer.ResumeTimer();
                };
            });
        }
        private void MoveToNextStageGrid(Stickman stickman, int row, int col)
        {
            stickman.AgentMode(() =>
            {
                stickman.SetStickmanState(StickmanState.CarryRunning);
                
                CurrentStage.stickmanGrid.GetSlot(row, col).ClearSlot();

                NextStage.stickmanGrid.GetSlot(row, col).FillSlot(stickman);
                        
                Bridge bridgeClosest = GeneralMethods.FindClosest(CurrentStage.bridgeHandler.bridges, 
                    stickman.Transform.position);
            
                stickman.CrossTheBridge(bridgeClosest, () =>
                {
                    stickman.moverPoint.Move(NextStage.stickmanGrid.GetSlot(row, col).objectHolder.position);

                    stickman.moverPoint.onDestinationReachedOnce = () =>
                    {
                        stickman.SetStickmanState(StickmanState.CarryIdle);
                        
                        CheckStickmanCount();
                        
                        stickman.RotateToFront();
                        
                        StartCoroutine(SetObstacleModeAsync(stickman, 1f));
                    };
                });
            });
        }

        private IEnumerator SetObstacleModeAsync(Stickman stickman, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            stickman.ObstacleMode(() =>
            {
                stickman.input.isInputActive = true;
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
            _currentStageIndex++;
        }
    }
}