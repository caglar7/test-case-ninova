using System;
using _GAME_.Scripts.BridgeModule;
using _GAME_.Scripts.Movement;
using _GAME_.Scripts.SlotModule;
using _GAME_.Scripts.StickmanGridModule;
using Template;
using UnityEngine;

namespace _GAME_.Scripts.StageModule
{
    public class Stage : BaseMono, IModuleInit
    {
        [Header("References")]
        public BridgeHandler bridgeHandler;
        public StickmanSlotHandler stickmanSlotHandler;
        public StickmanGrid stickmanGrid;
        public Points points;
        public GameObject borderBack;
        
        [Header("Settings")] 
        public bool isFinalStage;
        
        public void Init()
        {
            bridgeHandler.Init();
            stickmanGrid.Init();

            bridgeHandler.OnAllBridgesCompleted += HandleBridgesCompleted;
        }
        private void OnDisable()
        {   
            bridgeHandler.OnAllBridgesCompleted -= HandleBridgesCompleted;
        }

        private void HandleBridgesCompleted()
        {
            StageEvents.OnStageCompleted?.Invoke(this);
        }
    }
}