using System;
using System.Collections;
using System.Collections.Generic;
using _GAME_.Scripts.BrickModule;
using _GAME_.Scripts.BridgeModule;
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
        public AgentMoverPoint moverPoint;
        public AgentMoverPath moverPath;
        public BaseInventory inventory;
        public ColorComponent colorComponent;
        
        [Header("Resources")] 
        public GameObject brickPrefab;

        [Header("Other")] 
        public List<ColorType> colorsExcluded = new();
        
        
        public void Init()
        {
            input.Init();
            moverPoint.Init();
            moverPath.Init();
            inventory.Init();
            
            input.MouseDown += HandleInput;
            
            ObstacleMode();

            ColorType stickmanColor = GeneralMethods.GetRandomEnumValueExcluding(colorsExcluded);
            for (int i = 0; i < 6; i++)
            {
                AddBrick(stickmanColor);
            }
            colorComponent.SetColor(stickmanColor);
        }
        private void Update()
        {
            moverPoint.OnUpdate();
            moverPath.OnUpdate();
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
        private void AddBrick(ColorType color)
        {
            Brick brick = Instantiate(brickPrefab).GetComponent<Brick>();
            inventory.TryAddItem(brick);
            brick.colorComponent.SetColor(color);
        }

        
        private void TryMoveToSlot()
        {
            if (moverPoint.CanMoveToDestination(Points.instance.pointOutsideOfGrid.position))
            {
                if(ComponentFinder.instance.SlotHandler.TryGetEmptySlot(out Slot slotFound))
                {
                    slotFound.FillSlot(this);
                        
                    moverPoint.Move(slotFound.Transform.position);

                    moverPoint.onDestinationReachedOnce = HandleReachedSlot;
                }
            }
            else
            {
                print("stickman cannot move");
                ObstacleMode();
            }
        }

        private void HandleReachedSlot()
        {
            StartCoroutine(DropTilesOnRoad());
        }

        IEnumerator DropTilesOnRoad()
        {
            if (ComponentFinder.instance.BridgeHandler
                .TryGetAvailableBridge(colorComponent.currentColor, out Bridge bridge))
            {
                yield return new WaitForSeconds(0.35f);
            
                int itemCount = inventory.ItemList.Count;
                for (int i = 0; i < itemCount; i++)
                {
                    BaseMono item = inventory.ItemList[^1];
                    if (inventory.TryRemoveItem(item))
                    {
                        bridge.AddBrick((Brick) item);
                    }
                    
                    yield return new WaitForSeconds(0.07f);

                    if (bridge.IsBridgeComplete)
                    {
                        break;
                    }
                }
            }
        }
        public void CrossTheRoad(Bridge road)
        {
            moverPoint.Move(road.pathPoints[0]);
            moverPoint.onDestinationReachedOnce = () =>
            {
                moverPath.Move(road.pathPoints.GetRange(1, road.pathPoints.Count-1));
                moverPath.onDestinationReachedOnce = () =>
                {
                    Destroy(gameObject, 0.5f);
                };
            };
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