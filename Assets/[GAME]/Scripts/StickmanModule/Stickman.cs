using System;
using System.Collections;
using _GAME_.Scripts.BrickModule;
using _GAME_.Scripts.ComponentAccess;
using _GAME_.Scripts.Movement;
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
        public BaseMovement mover;
        public BaseInventory inventory;

        [Header("Resources")] 
        public GameObject brickPrefab;
        
        
        
        
        [Button]
        public void Init()
        {
            input.Init();
            mover.Init();
            inventory.Init();
            
            input.MouseDown += HandleInput;
            
            ObstacleMode();
            
            AddBrick();
            AddBrick();
        }
        private void Update()
        {
            mover.OnUpdate();
        }


        private void HandleInput()
        {
            // is there slot
            if (ComponentFinder.instance.SlotHandler.AreSlotsFull())
            {
                print("Slots full");
                return;
            }
            
            AgentMode(TryMoveToSlot);
        }

        private void AddBrick()
        {
            Brick brick = Instantiate(brickPrefab).GetComponent<Brick>();
            inventory.TryAddItem(brick);
        }

        
        private void TryMoveToSlot()
        {
            if (mover is BaseAgentMover agentMover)
            {
                if (agentMover.CanMoveToDestination(Points.instance.pointOutsideOfGrid.position))
                {
                    if(ComponentFinder.instance.SlotHandler.TryGetEmptySlot(out Slot slotFound))
                    {
                        slotFound.FillSlot(this);
                        
                        mover.Move(slotFound.Transform.position);

                        mover.onDestinationReachedOnce += () =>
                        {
                            int itemCount = inventory.ItemList.Count;
                            for (int i = 0; i < itemCount; i++)
                            {
                                BaseMono item = inventory.ItemList[^1];
                                if (inventory.TryRemoveItem(item))
                                {
                                    ComponentFinder.instance.BrickRoadHandler.brickRoads[0]
                                        .AddBrick((Brick) item);
                                }
                            }
                        };
                    }
                }
                else
                {
                    print("stickman cannot move");
                    ObstacleMode();
                }
            }
        }


        private void AgentMode(Action onSet = null)
        {
            NavMeshAgent.enabled = false;
            NavMeshObstacle.enabled = false;
            StartCoroutine(EnableAgentAfter(onSet));
        }
        IEnumerator EnableAgentAfter(Action onSet)
        {
            yield return new WaitForSeconds(.1f);
            NavMeshAgent.enabled = true;
            onSet?.Invoke();
        }
        

        [Button]
        private void ObstacleMode(Action onSet = null)
        {
            NavMeshAgent.enabled = false;
            NavMeshObstacle.enabled = false;
            StartCoroutine(EnableObstacleAfter(onSet));
        }
        IEnumerator EnableObstacleAfter(Action onSet)
        {
            yield return new WaitForSeconds(.1f);
            NavMeshObstacle.enabled = true;
            onSet?.Invoke();
        }

    }
}