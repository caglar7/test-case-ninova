using Template;

namespace _GAME_.Scripts.ComponentAccess
{
    public class ComponentFinder : Singleton<ComponentFinder>
    {
        private SlotHandler _slotHandler;

        public SlotHandler SlotHandler
        {
            get
            {
                if (_slotHandler == null)
                    _slotHandler = FindObjectOfType<SlotHandler>();

                return _slotHandler;
            }
        }
    }
}