using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FrontTower : Tower
{
    [field: SerializeField]
    public override int HP { get; set; }
    [field: SerializeField]
    public override int DMG { get; set; }
    [field: SerializeField]
    public override Transform target { get; set; }
    [field: SerializeField]
    public override GameObject projectile { get; set; }

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

    private void OnCollisionEnter(Collision collision)
    {

    }
}
