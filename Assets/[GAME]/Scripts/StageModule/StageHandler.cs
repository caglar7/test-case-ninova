using System.Collections.Generic;
using _GAME_.Scripts.BridgeModule;
using _GAME_.Scripts.StickmanModule;
using Sirenix.OdinInspector;
using Template;
using Unity.VisualScripting;

namespace _GAME_.Scripts.StageModule
{
    public class StageHandler : BaseMono, IModuleInit
    {
        public List<Stage> stages = new();

        public int currentStageIndex = 0;
        
        public Stage CurrentStage => stages[currentStageIndex];
        public Stage NextStage => stages[currentStageIndex + 1];
        
        
        public void Init()
        {
            InitStages();

            FillFirstStageWithStickmen();
        }
        [Button]
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
                });
            });
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

        private void InitStages()
        {
            foreach (Stage stage in stages)
            {
                stage.Init();
            }
        }
    }
}