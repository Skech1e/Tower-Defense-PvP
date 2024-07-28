using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Checkpost : MonoBehaviour
{
    [field: SerializeField]
    public Transform target { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(nameof(Tags.Player)))
        {
            NavMeshAgent agent;
            other.TryGetComponent(out agent);
            if(agent != null)
                agent.SetDestination(target.position);
        }
    }
}
