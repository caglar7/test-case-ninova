

using Template;
using UnityEngine;
using UnityEngine.AI;

public class SnapAgentToGround : MonoBehaviour, IModuleInit
{
    private string tagGround = "Ground";
    private float maxRayDistance = 5f;

    private NavMeshAgent _agent;
    private const float REF_OFFSET = 0f;
    private float _hitDistance;

    public void Init()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void OnUpdate()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo ,maxRayDistance))
        {
            _hitDistance = Vector3.Distance(transform.position, hitInfo.point);
            _agent.baseOffset = REF_OFFSET - _hitDistance;
        }
    }

}