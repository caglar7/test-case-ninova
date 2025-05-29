using _GAME_.Scripts.BridgeModule;
using _GAME_.Scripts.ComponentAccess;
using _GAME_.Scripts.StickmanModule;
using Template;

namespace _GAME_.Scripts.SlotModule
{
    public class StickmanSlotHandler : SlotHandler
    {
        public void ClearSlotWith(Stickman stickman)
        {
            foreach (Slot slot in slots)
            {
                if (slot.currentObject is Stickman s)
                {
                    if (s == stickman)
                    {
                        slot.ClearSlot();
                    }
                }
            }
        }
    }
}