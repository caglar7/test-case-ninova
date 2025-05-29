using System;
using System.Collections;
using System.Collections.Generic;
using _GAME_.Scripts.BrickModule;
using _GAME_.Scripts.BridgeModule;
using _GAME_.Scripts.ComponentAccess;
using _GAME_.Scripts.Movement;
using Sirenix.OdinInspector;
using Template;
using UnityEngine;
using Random = UnityEngine.Random;

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
        public Timer timer;
        
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
            for (int i = 0; i < Random.Range(6, 10); i++)
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
            Vector3 target = ComponentFinder.instance.StageHandler.CurrentStage.points.pointOutsideOfGrid.position;
            if (moverPoint.CanMoveToDestination(target))
            {
                if(ComponentFinder.instance.SlotHandler.TryGetEmptySlot(out Slot slotFound))
                {
                    slotFound.FillSlot(this);

                    ComponentFinder.instance.StageHandler.CurrentStage.stickmanGrid
                        .ClearSlotWith(this);
                    
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
            print(transform.name + "Dropping");
            TryDropBricks(HandleReachedSlot, Leave);
        }


        private void Leave()
        {
            ComponentFinder.instance.SlotHandler.ClearSlotWith(this);
            
            List<Vector3> exitPoints = new();
            exitPoints.Add(Transform.position + (Vector3.back * .7f));
            exitPoints.Add(ComponentFinder.instance.StageHandler.CurrentStage.points.pointExit.position);
            moverPath.Move(exitPoints);
            moverPath.onDestinationReachedOnce = () =>
            {
                Destroy(gameObject, .2f);
            };
        }
 

        private void TryDropBricks(Action onSomeRemaining, Action onDroppedAll)
        {
            if (ComponentFinder.instance.BridgeHandler
                .TryGetAvailableBridge(colorComponent.currentColor, out Bridge bridge))
            {
                StartCoroutine
                (
                    DropBricksOnBridge(
                        bridge, 
                        bridge.GetNextColorCount(),
                        onSomeRemaining,
                        onDroppedAll)            
                );
            }
            else
            {
                if (timer.OnTimerDone != null)
                    return;
                
                timer.RemoveListeners();
                timer.OnTimerDone += () =>
                {
                    TryDropBricks(HandleReachedSlot, StopTimerAndLeave);
                };
                timer.StartTimer();
            }
        }

        private void StopTimerAndLeave()
        {
            timer.PauseTimer();
            Leave();
        }

        IEnumerator DropBricksOnBridge(
            Bridge bridge,
            int dropCount,
            Action onSomeRemaining,
            Action onDroppedAll)
        {
            yield return new WaitForSeconds(0.35f);

            int count = Mathf.Clamp(dropCount, 0, inventory.ItemList.Count);
            
            for (int i = 0; i < count; i++)
            {
                BaseMono item = inventory.ItemList[^1];
                if (inventory.TryRemoveItem(item))
                {
                    bridge.AddBrick((Brick) item);
                }
                    
                yield return new WaitForSeconds(0.07f);
            }
            
            if(inventory.ItemList.Count == 0)
                onDroppedAll?.Invoke();
            else 
                onSomeRemaining?.Invoke();
        }
        public void CrossTheBridge(Bridge bridge, Action onCrossDone)
        {
            moverPoint.Move(bridge.pathPoints[0]);
            moverPoint.onDestinationReachedOnce = () =>
            {
                moverPath.Move(bridge.pathPoints.GetRange(1, bridge.pathPoints.Count-1));
                moverPath.onDestinationReachedOnce = onCrossDone;
            };
        }


        public void AgentMode(Action onSet = null)
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
        

        public void ObstacleMode(Action onSet = null)
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