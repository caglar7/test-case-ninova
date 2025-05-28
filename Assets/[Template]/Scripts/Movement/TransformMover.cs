

using System;
using UnityEngine;

public class TransformMover : BaseCharacterMover
{
    public override void Move(Vector3 moveDir)
    {
        Transform.position += Transform.right * moveDir.x * currentSpeed * Time.deltaTime;
        Transform.position += Transform.forward * moveDir.z * currentSpeed * Time.deltaTime;
    }
}