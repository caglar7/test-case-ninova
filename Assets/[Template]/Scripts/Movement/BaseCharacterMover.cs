



using System;
using UnityEngine;

public class BaseCharacterMover : BaseMovement
{
    //public GroundCheck groundCheck;
    private Vector3 velocity;

    public override void Init()
    {
        SetSpeed(MovementSettings.Instance.speedDefault);
    }
    
    public override void OnUpdate()
    {
        //groundCheck.OnUpdate();
    }

    public override void Jump()
    {
        //if(groundCheck.IsOnGround() == false) return;

        velocity = Rigidbody.velocity;
        velocity.y = 0f;
        Rigidbody.velocity = velocity;

        Rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
}