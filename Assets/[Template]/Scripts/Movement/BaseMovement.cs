

using System;
using System.Collections.Generic;
using Template;
using UnityEngine;

public abstract class BaseMovement : BaseMono 
{
    protected float currentSpeed;

    public Action onMovementStartedOnce;
    public Action onMovementStartedAlways;
    public Action onDestinationReachedOnce;
    public Action onDestinationReachedAlways;

    public virtual void Init()
    {
        onMovementStartedOnce = null;
        onMovementStartedAlways = null;
        onDestinationReachedOnce = null;
        onDestinationReachedAlways = null;
    }

    public virtual void OnUpdate()
    {
        
    }

    public virtual void Move(Vector2 moveDir)
    {

    }

    public virtual void Move(Transform point)
    {
        
    }

    public virtual void Move(Vector3 destination)
    {

    }

    public virtual void Move(Transform[] path)
    {

    }

    public virtual void Move(Vector3[] path)
    {
        
    }
    public virtual void Move(List<Vector3> path)
    {
        
    }

    // make this more smooth
    public virtual void RotateToY(float targetY)
    {
        Transform.rotation = Quaternion.Euler(0f, targetY, 0f);
    }
    public virtual void RotateToDirection(Vector3 dir)
    {
        Transform.LookAt(dir);
    }
    public virtual void Jump()
    {
        
    }

    public virtual void SetSpeed(float value)
    {
        currentSpeed = value;
    }
}