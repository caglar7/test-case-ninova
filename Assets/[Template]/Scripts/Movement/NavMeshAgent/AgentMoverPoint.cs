

using Sirenix.OdinInspector;
using UnityEngine;

public class AgentMoverPoint : BaseAgentMover 
{


    protected bool _reachedDestination = false;


    public override void OnUpdate()
    {
        HandleMoveToTarget();
    }

    [Button]
    public override void Move(Transform point) => Move(point.position);
    public override void Move(Vector3 destination)
    {
        if(CanMoveToDestination(destination) == true)
        {
            Agent.SetDestination(destination);

            _reachedDestination = false;

            InvokeStartEvents();
        }
        else
        {
            onMovementStartedOnce = null;
            onDestinationReachedOnce = null;
        }
    }

    private void HandleMoveToTarget()
    {
        if (IsMovingToTarget() == true)
        {
            if (IsInStoppingDistance() == true)
            {
                if(_reachedDestination == true) return;
                
                _reachedDestination = true;

                ReachedDestination();
            }
        }
    }

    private bool IsMovingToTarget()
    {
        return Agent != null && Agent.enabled && Agent.hasPath;
    }
}