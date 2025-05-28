using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Template
{
    public class BaseInputComponent : BaseMono, IModuleInit
    {
        [Header("Base")]
        public bool isInputActive = true;
        
        protected bool InitCalled = false;
        
        public void Init()
        {
            BaseComponentFinder.instance.InputComponentManager.inputComponents.Add(this);
            InitCalled = true;
        }

        private void OnDisable()
        {
            if (BaseComponentFinder.instance != null
                && BaseComponentFinder.instance.InputComponentManager.inputComponents.Contains(this))
            {
                BaseComponentFinder.instance.InputComponentManager.inputComponents.Remove(this);
            }
        }
    }
}