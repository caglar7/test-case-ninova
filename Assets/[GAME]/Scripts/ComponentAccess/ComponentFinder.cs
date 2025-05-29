using _GAME_.Scripts.BridgeModule;
using _GAME_.Scripts.SlotModule;
using Template;

namespace _GAME_.Scripts.ComponentAccess
{
    public class ComponentFinder : Singleton<ComponentFinder>
    {
        private StickmanSlotHandler _slotHandler;
        private BridgeHandler _bridgeHandler;

        public StickmanSlotHandler SlotHandler
        {
            get
            {
                if (_slotHandler == null)
                    _slotHandler = FindObjectOfType<StickmanSlotHandler>();

                return _slotHandler;
            }
        }
        
        public BridgeHandler BridgeHandler
        {
            get
            {
                if (_bridgeHandler == null)
                    _bridgeHandler = FindObjectOfType<BridgeHandler>();

                return _bridgeHandler;
            }
        }
    }
}