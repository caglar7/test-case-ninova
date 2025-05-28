using System;
using Sirenix.OdinInspector;
using Template;
using Unity.VisualScripting;
using UnityEngine;

namespace _GAME_.Scripts.StickmanModule
{
    public class Stickman : BaseMono, IModuleInit
    {
        [Header("References")] 
        public MouseDownInput input;

        [Button]
        public void Init()
        {
            input.Init();
            input.MouseDown += HandleInput;           
        }

        private void HandleInput()
        {
            print("input");
        }
    }
}