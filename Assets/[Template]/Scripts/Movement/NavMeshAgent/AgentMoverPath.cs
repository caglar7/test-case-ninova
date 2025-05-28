

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentMoverPath : BaseAgentMover 
{

    
    private List<Vector3> _pathPoints = new List<Vector3>();


    public override void OnUpdate()
    {
        HandleMoveAlongPath();
    }

    public override void Move(Transform[] path)
    {
        Vector3[] positions = path.Select(t => t.position).ToArray();

        Move(positions);
    }

    public override void Move(List<Vector3> path)
    {
        Move(path.ToArray());
    }

    public override void Move(Vector3[] path)
    {
        _pathPoints.Clear();

        _pathPoints.AddRange(path);

        Agent.SetDestination(_pathPoints[0]);

        InvokeStartEvents();
    }

    private void HandleMoveAlongPath()
    {
        if (IsMovingAlongPath() == true)
        {
            if (IsInStoppingDistance() == true)
            {
                ReachedPathPoint();
            }
        }
    }

    private void ReachedPathPoint()
    {
        _pathPoints.RemoveAt(0);

        if (_pathPoints.Count > 0)
        {
            Agent.SetDestination(_pathPoints[0]);
        }
        else ReachedDestination();
    }

    private bool IsMovingAlongPath()
    {
        return Agent != null && Agent.enabled && _pathPoints.Count > 0;
    }
}