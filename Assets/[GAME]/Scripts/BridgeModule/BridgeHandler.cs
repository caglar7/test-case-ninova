using System.Collections.Generic;
using Template;
using UnityEngine;
using UnityEngine.Serialization;

namespace _GAME_.Scripts.BridgeModule
{
    public class BridgeHandler : BaseMono, IModuleInit
    {
        public List<Bridge> bridges = new();
        
        [SerializeField] private List<Bridge> availableBridges;


        public void Init()
        {
            foreach (Bridge bridge in bridges)
            {
                bridge.Init();
            }
        }


        public bool TryGetAvailableBridge(ColorType color, out Bridge bridge)
        {
            availableBridges = new();
             
            foreach (Bridge b in bridges)
            {
                if (b.IsBridgeComplete)
                    continue;
                
                if (b.BrickBlueprints[b.NextBlueprintIndex].colorComponent.currentColor == color)
                {
                    availableBridges.Add(b);
                }
            }
                      
            availableBridges.Sort((bridgeA, bridgeB) =>
                bridgeA.BrickCount > bridgeB.BrickCount ? 1 : -1
            );

            if (availableBridges.Count > 0)
            {
                bridge = availableBridges[0];
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
    }
}