using _GAME_.Scripts.BrickRoadModule;
using Template;

namespace _GAME_.Scripts.ComponentAccess
{
    public class ComponentFinder : Singleton<ComponentFinder>
    {
        private SlotHandler _slotHandler;
        private BrickRoadHandler _brickRoadHandler;

        public SlotHandler SlotHandler
        {
            get
            {
                if (_slotHandler == null)
                    _slotHandler = FindObjectOfType<SlotHandler>();

                return _slotHandler;
            }
        }
        
        public BrickRoadHandler BrickRoadHandler
        {
            get
            {
                if (_brickRoadHandler == null)
                    _brickRoadHandler = FindObjectOfType<BrickRoadHandler>();

                return _brickRoadHandler;
            }
        }
    }
}