using _GAME_.Scripts.BridgeModule;
using _GAME_.Scripts.ComponentAccess;
using _GAME_.Scripts.StickmanModule;
using Template;

namespace _GAME_.Scripts.SlotModule
{
    public class StickmanSlotHandler : SlotHandler
    {
        public Timer timer;

        public override void Init()
        {
            base.Init();
            
            timer.RemoveListeners();
            timer.OnTimerDone += HandleTimerDone;
            timer.StartTimer();
        }

        private void HandleTimerDone()
        {
            // check slots and check brick roads 
            // ...

            // foreach (Bridge brickRoad in ComponentFinder.instance.BridgeHandler.bridges)
            // {
            //     if (brickRoad.IsRoadCompleted)
            //     {
            //         foreach (Slot slot in ComponentFinder.instance.SlotHandler.slots)
            //         {
            //             if (slot.currentObject is Stickman stickman)
            //             {
            //                 if (stickman.colorComponent.currentColor == brickRoad.currentColor)
            //                 {
            //                     slot.ClearSlot();
            //                     stickman.CrossTheRoad(brickRoad);
            //                 }
            //             }
            //         }
            //     }
            // }
        }

        public override void OnDisable()
        {
            base.OnDisable();
            
        }
    }
}