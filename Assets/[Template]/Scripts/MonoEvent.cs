

using UnityEngine;

namespace Template
{
    public abstract class MonoEvent : MonoBehaviour
    {
        private void OnEnable() {
            RegisterToEvents(); 
        }

        private void OnDisable() {
            UnRegisterToEvents();
        }

        public abstract void RegisterToEvents();
        public abstract void UnRegisterToEvents();
    }
}