

using UnityEngine;

public class TransformRaycastMover : TransformMover 
{
    public Transform raycastPivot;
    public float checkDistance = 1f;
    
    private LayerMask _layerMask;
    private Vector3 _currentMoveDir;

    public override void Init()
    {
        base.Init();

        _layerMask = LayerMask.GetMask("PlayerBlockCollider");
    }

    public override void Move(Vector3 localMoveDir)
    {
        _currentMoveDir = Transform.TransformDirection(localMoveDir);

        if (Physics.Raycast(raycastPivot.position, _currentMoveDir, checkDistance, _layerMask, QueryTriggerInteraction.Ignore)) 
            return;  

        base.Move(localMoveDir);
    }
}