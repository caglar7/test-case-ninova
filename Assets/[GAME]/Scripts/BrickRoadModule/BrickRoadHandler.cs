using System;
using System.Collections.Generic;
using Template;

namespace _GAME_.Scripts.BrickRoadModule
{
    public class BrickRoadHandler : BaseMono, IModuleInit
    {
        public List<BrickRoad> brickRoads = new();


        public void Init()
        {
            foreach (BrickRoad road in brickRoads)
            {
                road.Init();
            }
        }


        public bool TryGetAvailableRoad(ColorType color, out BrickRoad road)
        {
            road = brickRoads[0];
            return true;
        }
    }
}