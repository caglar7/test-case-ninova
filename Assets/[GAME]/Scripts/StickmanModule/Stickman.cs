using System;
using System.Collections;
using System.Collections.Generic;
using _GAME_.Scripts.BrickModule;
using _GAME_.Scripts.BridgeModule;
using _GAME_.Scripts.ComponentAccess;
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
        public StickmanAnimation stickmanAnimation;
        
        [Header("Resources")] 
        public GameObject brickPrefab;

        [Header("Other")] 
        public List<ColorType> colorsExcluded = new();

        public StickmanState currentState;
        
        private Action<StickmanState> _onStateChanged;
        private Action _currentOnDroppedAll;
        private Action _currentOnSomeRemaining;

        public void Init()
        {
            input.Init();
            moverPoint.Init();
            moverPath.Init();
            inventory.Init();
            
            input.MouseDown += HandleInput;
            _onStateChanged = null;
            _onStateChanged += stickmanAnimation.HandleAnimation;
            
            SetStickmanState(StickmanState.CarryIdle); 
            ObstacleMode();
        }
        private void Update()
        {
            moverPoint.OnUpdate();
            moverPath.OnUpdate();
        }

        private void OnDisable()
        {
            timer.RemoveListeners();
            timer.PauseTimer();
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
        public void SetStickmanState(StickmanState state)
        {
            currentState = state;
            _onStateChanged?.Invoke(currentState);
        }
        public void AgentMode(Action onSet = null)
        {
            NavMeshAgent.enabled = false;
            NavMeshObstacle.enabled = false;
            StartCoroutine(EnableAgentAfter(onSet));
        }
        public void ObstacleMode(Action onSet = null)
        {
            NavMeshAgent.enabled = false;
            NavMeshObstacle.enabled = false;
            StartCoroutine(EnableObstacleAfter(onSet));
        }
        public void AddBrick()
        {
            Brick brick = Instantiate(brickPrefab).GetComponent<Brick>();
            inventory.TryAddItem(brick);
        }
        public void AddBrick(ColorType color)
        {
            Brick brick = Instantiate(brickPrefab).GetComponent<Brick>();
            inventory.TryAddItem(brick);
            brick.colorComponent.SetColor(color);
        }
        public void AddBricks(ColorType color, int amount)
        {
            StartCoroutine(AddBricksAsync(color, amount));
        }
        IEnumerator AddBricksAsync(ColorType color, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Brick brick = Instantiate(brickPrefab).GetComponent<Brick>();
                inventory.TryAddItem(brick);
                brick.colorComponent.SetColor(color);
                yield return null;
            }
        }
        public void RotateToFront(Action onDone = null)
        {
            TweenRotation.RotateGlobal(
                this, 
                Vector3.zero, 
                StickmanSettings.Instance.rotationToFrontSettings,
                onDone);
        }
        public void RotateToBack(Action onDone = null)
        {
            TweenRotation.RotateGlobal(
                this, 
                new Vector3(0f, 180f, 0f), 
                StickmanSettings.Instance.rotationToBackSettings,
                onDone);
        }
        
        

        private void HandleInput()
        {
            // is there slot
            if (ComponentFinder.instance.SlotHandler.AreSlotsFull())
            {
                return;
            }
            
            AgentMode(TryMoveToSlot);
        }
        private void TryMoveToSlot()
        {
            Vector3 target = ComponentFinder.instance.StageHandler.CurrentStage.points.pointOutsideOfGrid.position;
            if (moverPoint.CanMoveToDestination(target))
            {
                if(ComponentFinder.instance.SlotHandler.TryGetEmptySlot(out Slot slotFound))
                {
                    SetStickmanState(StickmanState.CarryRunning);
                    
                    slotFound.FillSlot(this);

                    ComponentFinder.instance.StageHandler.CurrentStage.stickmanGrid
                        .ClearSlotWith(this);
                    
                    moverPoint.Move(slotFound.objectHolder.position);

                    moverPoint.onDestinationReachedOnce = () =>
                    {
                        SetStickmanState(StickmanState.CarryIdle);

                        RotateToFront(HandleReachedSlot);
                    };

                    input.isInputActive = false;
                    
                    StickmanEvents.OnMadeMove?.Invoke();
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
            TryDropBricks(HandleReachedSlot, RotateAndLeave);
        }
        
 

        private void TryDropBricks(Action onSomeRemaining, Action onDroppedAll)
        {
            if (ComponentFinder.instance.BridgeHandler
                .TryGetAvailableBridge(colorComponent.currentColor, out Bridge bridge))
            {
                DropBricks(
                    bridge,
                    bridge.GetNextColorCount(),
                    onSomeRemaining, 
                    onDroppedAll
                );
            }
            else
            {
                if (timer.OnTimerDone != null)
                    return;
                
                timer.RemoveListeners();
                timer.OnTimerDone += () =>
                {
                    TryDropBricks(HandleReachedSlot, StopTimerAndRotateAndLeave);
                };
                timer.StartTimer();
            }
        }

        private void StopTimerAndRotateAndLeave()
        {
            timer.PauseTimer();
            RotateAndLeave();
        }
        
        private void RotateAndLeave()
        {
            RotateToBack(Leave);
        }
        private void Leave()
        {
            SetStickmanState(StickmanState.Running);
            
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
        

        private void DropBricks(
            Bridge bridge,
            int dropCount,
            Action onSomeRemaining,
            Action onDroppedAll)
        {
            _currentOnSomeRemaining = onSomeRemaining;
            _currentOnDroppedAll = onDroppedAll;
            
            int count = Mathf.Clamp(dropCount, 0, inventory.ItemList.Count);

            for (int i = 0; i < count; i++)
            {
                var item = inventory.ItemList[^1];

                if (inventory.TryRemoveItem(item))
                {
                    bridge.AddBrick(
                        (Brick) item, 
                        i * 0.07f, 
                        (i == count-1) ? HandleLastDrop : null
                    );
                }
            }
        }
        private void HandleLastDrop()
        {
            if(inventory.ItemList.Count == 0)
                _currentOnDroppedAll?.Invoke();
            else 
                _currentOnSomeRemaining?.Invoke();
        }
        

        IEnumerator EnableAgentAfter(Action onSet)
        {
            yield return new WaitForSeconds(.1f);
            NavMeshAgent.enabled = true;
            onSet?.Invoke();
        }
        
        IEnumerator EnableObstacleAfter(Action onSet)
        {
            yield return new WaitForSeconds(.1f);
            NavMeshObstacle.enabled = true;
            onSet?.Invoke();
        }
    }
}