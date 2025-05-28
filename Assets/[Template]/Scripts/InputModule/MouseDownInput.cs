using System;
using UnityEngine;

namespace Template
{
    public class MouseDownInput : BaseInputComponent
    {
        public Action MouseDown;

        private void OnMouseDown()
        {
            if (InitCalled == false)
            {
                Debug.LogError("BaseInputComponent Init() must be called");
                return;
            }
            
            if (isInputActive == false)
                return;
            
            MouseDown?.Invoke();
        }
    }
}