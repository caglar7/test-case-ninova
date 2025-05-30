using System;
using System.Collections.Generic;
using Template;
using UnityEngine;
using UnityEngine.Serialization;

namespace _GAME_.Scripts.BridgeModule
{
    public class BridgeHandler : BaseMono, IModuleInit
    {
        public List<Bridge> bridges = new();
        
        private List<Bridge> _availableBridges = new();

        private int _completedBridges;

        public Action OnAllBridgesCompleted;
        
        public void Init()
        {
            _completedBridges = 0;
            
            foreach (Bridge bridge in bridges)
            {
                bridge.Init();
                bridge.OnBridgeCompleted += HandleBridgeCompleted;
            }

            OnAllBridgesCompleted = null;
        }

        public bool TryGetAvailableBridge(ColorType color, out Bridge bridge)
        {
            _availableBridges = new();
             
            foreach (Bridge b in bridges)
            {
                if (b.IsBridgeComplete)
                    continue;
                
                if (b.BrickBlueprints[b.NextBlueprintIndex].colorComponent.currentColor == color)
                {
                    _availableBridges.Add(b);
                }
            }
                      
            _availableBridges.Sort((bridgeA, bridgeB) =>
                bridgeA.BrickCount > bridgeB.BrickCount ? 1 : -1
            );

            if (_availableBridges.Count > 0)
            {
                bridge = _availableBridges[0];
                return true;
            }
            else
            {
                bridge = null;
                return false;
            }
        }

        public bool AreAllBridgesComplete()
        {
            foreach (Bridge bridge in bridges)
            {
                if (bridge.IsBridgeComplete == false)
                    return false;
            }

            return true;
        }
        
        
        private void HandleBridgeCompleted()
        {
            _completedBridges++;

            if (_completedBridges >= bridges.Count)
            {
                OnAllBridgesCompleted?.Invoke();
            }
        }
    }
}