using System.Collections.Generic;
using _GAME_.Scripts.StickmanModule;
using Template.ObjectGridModule;
using UnityEngine;

namespace _GAME_.Scripts.StickmanGridModule
{
    public class StickmanGrid : ObjectGrid
    {
        [Header("Stickman Grid")] 
        public GameObject stickmanPrefab;

        private List<Stickman> _stickmans = new();
        public Transform testMovePoint;
        
        
        public override void Init()
        {
            base.Init();

            FillGridWithStickmans();
        }

        private void FillGridWithStickmans()
        {
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    Stickman stickman = Instantiate(stickmanPrefab).GetComponent<Stickman>();

                    stickman.Init();

                    stickman.Transform.name = "Stickman " +  (_stickmans.Count + 1).ToString();
                    
                    FillSlot(stickman, row, column);
                    
                    _stickmans.Add(stickman);

                    // testing
                    stickman.testPoint = testMovePoint;
                }
            }
        }
    }
}