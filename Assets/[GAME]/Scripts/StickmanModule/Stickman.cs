using System;
using System.Collections;
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

        public Transform testPoint;
        public float delay = .1f;
        
        
        [Button]
        public void Init()
        {
            input.Init();
            input.MouseDown += HandleInput;
            
            mover.Init();
            
            ObstacleMode();
        }

        
        private void HandleInput()
        {
            AgentMode(MoveToTestPoint);
        }
        
        
        
        [Button]
        private void AgentMode(Action onSet = null)
        {
            NavMeshAgent.enabled = false;
            NavMeshObstacle.enabled = false;
            StartCoroutine(EnableAgentAfter(onSet));
        }
        IEnumerator EnableAgentAfter(Action onSet)
        {
            yield return new WaitForSeconds(delay);
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
            yield return new WaitForSeconds(delay);
            NavMeshObstacle.enabled = true;
            onSet?.Invoke();
        }

        


        private void MoveToTestPoint()
        {
            mover.Move(testPoint);
        }
    }
}