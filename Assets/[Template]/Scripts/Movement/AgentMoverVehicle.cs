


using System.Collections.Generic;
using Sirenix.OdinInspector;
using Template;
using UnityEngine;

public class AgentMoverVehicle : AgentMoverPath 
{
    public int unitAngle = 30;
    public float unitDistance = 2f;

    private float unitDistanceX => Mathf.Sin(Mathf.Deg2Rad * unitAngle);
    private float unitDistanceZ => Mathf.Cos(Mathf.Deg2Rad * unitAngle);
    private List<Vector3> points = new List<Vector3>();
    private List<Vector3> turningPoints = new List<Vector3>();
    private Vector3 localDestination, dirToDestination;
    private float dotProduct;


    public override void Move(Transform point) => Move(point.position);
    public override void Move(Vector3 destination)
    {
        points.Clear();

        localDestination = Transform.InverseTransformPoint(destination);

        dirToDestination = (destination - Transform.position).normalized;

        dotProduct = Vector3.Dot(Transform.forward, dirToDestination);

        if (dotProduct >= 0)
        {
            points.Add(destination);
            Move(points);
        }
        else
        {
            points.AddRange(GetPointsForTurning());
            points.Add(destination);
            Move(points);
        }
    }

    private List<Vector3> GetPointsForTurning()
    {
        turningPoints.Clear();

        float angleBetween = Vector3.Angle(Transform.forward, dirToDestination);
        Vector3 pos = Transform.position;
        Vector3 dir = Transform.forward;
        float angleMinus90 = angleBetween - 90f;

        for (int i = 0; i < angleMinus90; i += unitAngle)
        {
            pos += VectorUtility.GetLocalXZ(dir, DestinationLocalRight() * unitDistanceX, unitDistanceZ);
            dir = VectorUtility.RotateAroundY(dir, DestinationLocalRight() * unitAngle);
            turningPoints.Add(pos);
        }

        return turningPoints;
    }

    private int DestinationLocalRight()
    {
        return (localDestination.x >= 0) ? 1 : -1;
    }
}