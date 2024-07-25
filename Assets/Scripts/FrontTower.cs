using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FrontTower : Tower
{
    [field: SerializeField] public override byte HP { get; set; }
    [field: SerializeField] public override byte DMG { get; set; }
    [field: SerializeField, Range(0.1f, 10f)] public override float Range { get; set; }
    [field: SerializeField] public override Transform target { get; set; }
    [field: SerializeField] public override Projectile projectile { get; set; }
    public override Transform shooter { get; set; }

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
