

// async calls

using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Template
{
    public abstract class GameStateManager : BaseMono, IModuleInit, IEvents
    {
        public GameState currentState;
        public GameState previousState;
        
        [Space]
        [Header("UNITY EVENTS")] 
        public UnityEvent onLevelStarted;
        public UnityEvent onLevelCompleted;
        public UnityEvent onLevelFailed;
        
        
        
        public virtual void Init()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
        }

        
        

        protected void CheckComplete()
        {
            if (currentState == GameState.Failed || currentState == GameState.Completed) // avoid multiple calls
                return;
            
            if (IsCompleted())
            {
                SetState(GameState.Completed);
                
                GeneralUtils.Delay(GameStateSettings.Instance.delayLevelComplete, () =>
                {
                    GameStateEvents.OnLevelCompleted?.Invoke();
                    onLevelCompleted?.Invoke();
                });
            }
        }
        
        protected void CheckFail()
        {
            if (currentState == GameState.Failed || currentState == GameState.Completed) // avoid multiple calls
                return;
            
            if (IsFailed() == true)
            {
                SetState(GameState.Failed);
                
                GeneralUtils.Delay(GameStateSettings.Instance.delayLevelFailed, () =>
                {
                    GameStateEvents.OnLevelFailed?.Invoke();
                    onLevelFailed?.Invoke();
                });
            }
        }
        
        
        public void SetState(GameState state)
        {
            previousState = currentState;
            currentState = state;
            print("current state: " + currentState.ToString());
        }
        
        
        
        protected virtual void LevelStarted(int fakeLevelIndex)
        {
            SetState(GameState.Playing);
            GameStateEvents.OnLevelStarted?.Invoke();
            onLevelStarted?.Invoke();
        }
        public abstract void SubscribeToEvents();
        public abstract void UnsubscribeToEvents();
        public abstract bool IsCompleted();
        public abstract bool IsFailed();
    }
}