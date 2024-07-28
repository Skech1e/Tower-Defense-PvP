using UnityEngine;
using UnityEngine.AI;
using static Logger;

[RequireComponent(typeof(SphereCollider))]
public class FrontTower : Tower
{
    [field: SerializeField] public override byte HP { get; set; }
    [field: SerializeField] public override byte DMG { get; set; }
    [field: SerializeField, Range(0.1f, 10f)] public override float Range { get; set; }
    //[field: SerializeField] public override Transform target { get; set; }
    [field: SerializeField] public override Projectile projectile { get; set; }
    public override Transform shooter { get; set; }

    [field: SerializeField] public Transform target { get; private set; }
    SphereCollider Aoe;
    private void Awake()
    {
        Aoe = GetComponent<SphereCollider>();
        Aoe.isTrigger = true;
        Aoe.radius = 3f;
    }
    public override void Attack(Transform target)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(nameof(Tags.Player)))
        {
            NavMeshAgent agent;
            other.TryGetComponent(out agent);
            if (agent != null)
            {
                agent.SetDestination(target.position);
                L.Log(agent.destination);
            }
        }
    }
}
