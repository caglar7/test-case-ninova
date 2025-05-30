using _GAME_.Scripts.BridgeModule;
using _GAME_.Scripts.FinishModule;
using _GAME_.Scripts.SlotModule;
using _GAME_.Scripts.StageModule;
using Template;

namespace _GAME_.Scripts.ComponentAccess
{
    public class ComponentFinder : Singleton<ComponentFinder>
    {
        private StickmanSlotHandler _slotHandler;
        private BridgeHandler _bridgeHandler;
        private StageHandler _stageHandler;
        private FinishHandler _finishHandler;

        public FinishHandler FinishHandler
        {
            get
            {
                if (_finishHandler == null)
                    _finishHandler = FindObjectOfType<FinishHandler>();

                return _finishHandler;
            }
        }
        public StageHandler StageHandler
        {
            get
            {
                if (_stageHandler == null)
                    _stageHandler = FindObjectOfType<StageHandler>();

                return _stageHandler;
            }
        }
        
        public StickmanSlotHandler SlotHandler => StageHandler.CurrentStage.stickmanSlotHandler;

        public BridgeHandler BridgeHandler => StageHandler.CurrentStage.bridgeHandler;
    }
}