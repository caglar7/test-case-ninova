


using System;
using UnityEngine;

public class RBMover : BaseCharacterMover
{
    public override void Move(Vector3 moveDir)
    {
        // Rigidbody.position += TransformCached.right * moveDir.x * currentSpeed * Time.deltaTime;
        // Rigidbody.position += TransformCached.forward * moveDir.z * currentSpeed * Time.deltaTime;

        // Convert the local movement direction to world space
        Vector3 movement = Transform.TransformDirection(new Vector3(moveDir.x, 0, moveDir.z));

        // Apply movement to Rigidbody position
        Rigidbody.position += movement * currentSpeed * Time.deltaTime;
    }
}