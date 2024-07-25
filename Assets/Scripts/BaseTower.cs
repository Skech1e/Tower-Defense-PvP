using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BaseTower : Tower
{
    [field: SerializeField] public override byte HP { get; set; }
    [field: SerializeField] public override byte DMG { get; set; }
    [field: SerializeField, Range(0.1f, 10f)] public override float Range { get; set; }
    [field: SerializeField] public override Transform target { get; set; }
    [field: SerializeField] public override Projectile projectile { get; set; }
    public override Transform shooter { get; set; }

    Projectile orb;
    public bool canShoot;

    [Range(0f, 5f)] public float speed;

    SphereCollider Aoe;
    private void Awake()
    {
        Aoe = GetComponent<SphereCollider>();
        Aoe.isTrigger = true;
        Aoe.radius = Range;
        shooter = transform.GetChild(0);
    }
    public override void Attack(Transform target)
    {
        orb = Instantiate(projectile, shooter, false);
        orb.speed = speed;
        orb.target = target;
        orb.dmg = DMG;
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Enemy") && orb == null)
        {
            target = other.transform;
            Attack(target);
        }
    }

}
