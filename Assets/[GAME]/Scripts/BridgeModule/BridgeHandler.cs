using System.Collections.Generic;
using Template;
using UnityEngine.Serialization;

namespace _GAME_.Scripts.BridgeModule
{
    public class BridgeHandler : BaseMono, IModuleInit
    {
        public List<Bridge> bridges = new();


        public void Init()
        {
            foreach (Bridge road in bridges)
            {
                road.Init();
            }
        }


        public bool TryGetAvailableBridge(ColorType color, out Bridge road)
        {
            // foreach (Bridge brickRoad in bridges)
            // {
            //     if (brickRoad.currentColor == color)
            //     {
            //         if (brickRoad.IsRoadCompleted == false)
            //         {
            //             road = brickRoad;
            //             return true;
            //         }
            //     }
            // }

            road = null;
            return false;
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