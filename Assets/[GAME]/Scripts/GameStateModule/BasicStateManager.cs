using _GAME_.Scripts.ComponentAccess;
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
            timerFail.RemoveListeners();
            timerFail.OnTimerDone += CheckFail;
            StickmanEvents.OnMadeMove += timerFail.StartTimer;
        }

        public override void UnsubscribeToEvents()
        {
            StickmanEvents.OnMadeMove -= timerFail.StartTimer;
        }

        public override bool IsCompleted()
        {
            // to do
            return false;
        }

        public override bool IsFailed()
        {
            return ComponentFinder.instance.SlotHandler.AreSlotsFull();
        }
    }
}