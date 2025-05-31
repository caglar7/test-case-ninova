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
using UnityEngine.Serialization;

namespace _GAME_.Scripts.StageModule
{
    public class StageHandler : BaseMono, IModuleInit
    {
        public List<Stage> stages = new();
        
        public float camBlendDuration = 1f;
        public float finishDuration = 2f;
        
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
            StageEvents.StageTransitionStarted?.Invoke();
            
            if (stage.isFinalStage)
            {
                FinishLevel();
                return;
            }
            
            GetStickmanCounts();

            MoveStickmansFromSlots();
            
            MoveStickmansFromGrid();  
            
            MoveCamera(camBlendDuration);
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
            for (int col = 0; col < CurrentStage.stickmanGrid.columnCount; col++)
            {
                for (int row = 0; row < CurrentStage.stickmanGrid.rowCount; row++)
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
            for (int col = 0; col < CurrentStage.stickmanGrid.columnCount; col++)
            {
                for (int row = 0; row < CurrentStage.stickmanGrid.rowCount; row++)
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
        private void MoveCamera(float blendDuration)
        {
            BaseComponentFinder.instance.CameraManager.ChangeAngle(
                _currentStageIndex + 1, 
                blendDuration
            );
        }

        private void FinishLevel()
        {
            GetRemainingStickmans();
            
            MoveStickmansToFinishLine();

            MoveCamera(camBlendDuration);
            
            InvokeDoneAfterDelay();
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
                        if(stickman.inventory.ItemList.Count > 0)
                            _stickmansRemaining.Add(stickman);
                    }
                }
            }
            for (int col = 0; col < CurrentStage.stickmanGrid.columnCount; col++)
            {
                for (int row = 0; row < CurrentStage.stickmanGrid.rowCount; row++)
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
                        i 
                    );
                }
            }
        }
        private void MoveStickmanToFinishSlot(Stickman stickman, int slotIndex, Action onMoveDone = null)
        {
            stickman.AgentMode(() =>
            {
                ComponentFinder.instance.FinishHandler.finishSlotHandler.slots[slotIndex]
                    .FillSlot(stickman);
            
                Bridge bridgeClosest = GeneralMethods.FindClosest(CurrentStage.bridgeHandler.bridges, 
                    stickman.Transform.position);
            
                stickman.SetStickmanState(StickmanState.CarryRunning);
            
                stickman.CrossTheBridge(bridgeClosest, () =>
                {
                    stickman.EnableUpdate();
                    stickman.moverPoint.Move(
                        ComponentFinder.instance.FinishHandler.finishSlotHandler.slots[slotIndex].Transform.position
                    );

                    stickman.moverPoint.onDestinationReachedOnce = () =>
                    {
                        stickman.DisableUpdate();
                        
                        stickman.SetStickmanState(StickmanState.CarryIdle);
                    
                        onMoveDone?.Invoke();
                    };
                });
            });
        }
        private void InvokeDoneAfterDelay()
        {
            GeneralUtils.Delay(finishDuration, InvokeDoneEvent);
        }
        private void InvokeDoneEvent()
        {
            StageEvents.OnAllStagesDone?.Invoke();
        }


        private void MoveToNextStageSlot(Stickman stickman, Bridge bridgeClosest, int slotIndex)
        {
            stickman.SetStickmanState(StickmanState.CarryRunning);
            
            stickman.timer.PauseTimer();
            
            stickman.CrossTheBridge(bridgeClosest, () =>
            {
                stickman.EnableUpdate();
                stickman.moverPoint.Move(NextStage.stickmanSlotHandler.slots[slotIndex].objectHolder.position);
                
                stickman.moverPoint.onDestinationReachedOnce = () =>
                {
                    stickman.DisableUpdate();
                    
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
                    stickman.EnableUpdate();
                    stickman.moverPoint.Move(NextStage.stickmanGrid.GetSlot(row, col).objectHolder.position);

                    stickman.moverPoint.onDestinationReachedOnce = () =>
                    {
                        stickman.DisableUpdate();
                        
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
                ActivateStageBorder();
            }
        }

        private void SetToNextStage()
        {
            _currentStageIndex++;
        }
        private void ActivateStageBorder()
        {
            GeneralUtils.Delay(1f, () =>
            {
                CurrentStage.borderBack.SetActive(true);            
            });
        }
    }
}