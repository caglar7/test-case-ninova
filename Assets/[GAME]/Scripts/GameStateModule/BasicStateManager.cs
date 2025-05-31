using _GAME_.Scripts.ComponentAccess;
using _GAME_.Scripts.StageModule;
using _GAME_.Scripts.StickmanModule;
using Template;
using UnityEngine.Serialization;

namespace _GAME_.Scripts.GameStateModule
{
    public class BasicStateManager : GameStateManager
    {
        public Timer timerFail;
        
        public override void SubscribeToEvents()
        {
            LevelEvents.OnLevelLoaded += LevelStarted;
            
            timerFail.RemoveListeners();
            timerFail.OnTimerDone += CheckFail;
            StickmanEvents.OnMadeMove += timerFail.StartTimer;
            
            StageEvents.OnAllStagesDone += CheckComplete;

            StageEvents.StageTransitionStarted += HandleStageTransitionStarted;
        }

        public override void UnsubscribeToEvents()
        {
            LevelEvents.OnLevelLoaded -= LevelStarted;
            
            StickmanEvents.OnMadeMove -= timerFail.StartTimer;
            
            StageEvents.OnAllStagesDone -= CheckComplete;
            
            StageEvents.StageTransitionStarted -= HandleStageTransitionStarted;
        }

        private void HandleStageTransitionStarted()
        {
            timerFail.PauseTimer();
        }

        public override bool IsCompleted()
        {
            return true;
        }

        public override bool IsFailed()
        {
            return ComponentFinder.instance.SlotHandler.AreSlotsFull();
        }
    }
}