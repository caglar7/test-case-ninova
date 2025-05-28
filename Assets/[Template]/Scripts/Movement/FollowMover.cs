


using Template;
using UnityEngine;

public class FollowMover : BaseMovement 
{
    public Transform followPoint;

    public override void OnUpdate()
    {
        Move(followPoint);
    }

    public override void Move(Transform point)
    {
        Transform.position = VectorUtility.GetSelectedAxisFromTarget(Transform.position, 
                                        point.position, new Vector3(1f, 0f, 1f));

        Transform.rotation = Quaternion.Lerp(Transform.rotation, 
                                        point.rotation, Time.deltaTime * 5f);
    }
}