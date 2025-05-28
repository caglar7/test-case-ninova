using _GAME_.Scripts.BridgeModule;
using Template;

namespace _GAME_.Scripts.ComponentAccess
{
    public class ComponentFinder : Singleton<ComponentFinder>
    {
        private SlotHandler _slotHandler;
        private BridgeHandler _bridgeHandler;

        public SlotHandler SlotHandler
        {
            get
            {
                if (_slotHandler == null)
                    _slotHandler = FindObjectOfType<SlotHandler>();

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