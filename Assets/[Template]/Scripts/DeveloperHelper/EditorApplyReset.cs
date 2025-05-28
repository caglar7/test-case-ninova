using UnityEngine;

namespace Template.Scripts.DeveloperHelper
{
    public abstract class EditorApplyReset : BaseMono
    {
        [Header("Edit")] 
        public bool apply = false;
        public bool reset = false;
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            _OnValidate();
        }

        private void _OnValidate()
        {
            if (reset == true)
            {
                reset = false;

                _Reset();
            }

            if (apply == true)
            {
                apply = false;

                _Apply();
            }
        }

#endif

        
        
        protected abstract void _Apply();
        protected abstract void _Reset();
    }
}