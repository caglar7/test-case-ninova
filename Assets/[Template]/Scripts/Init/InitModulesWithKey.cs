


using UnityEngine;

namespace Template
{
    public class InitModulesWithKey : BaseInitModules, IInputKeyDown
    {
        [Header("Subclass Settings")]
        public KeyCode whichKey;

        private void Start() 
        {
            RegisterToInputEvents();
        }
        private void OnDisable() 
        {
            UnRegisterToInputEvents();
        }
    
        public void RegisterToInputEvents()
        {
            InputManager.instance.onKeyDown += HandleKeyDownInput;
        }

        public void UnRegisterToInputEvents()
        {
            if(InputManager.instance != null)
                InputManager.instance.onKeyDown -= HandleKeyDownInput;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void HandleKeyDownInput(KeyCode key)
        {
            if (key == whichKey)
            {
                Init();
            }
        }
    }
}