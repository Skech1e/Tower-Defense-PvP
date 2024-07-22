using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BaseTower : Tower
{
    [field: SerializeField]
    public override int HP { get; set; }
    [field: SerializeField]
    public override int DMG { get; set; }
    [field: SerializeField]
    public override Transform target { get; set; }
    [field: SerializeField]
    public override GameObject projectile { get; set; }
    GameObject proj;

    SphereCollider Aoe;
    private void Awake()
    {
        Aoe = GetComponent<SphereCollider>();
        Aoe.isTrigger = true;
        Aoe.radius = 3f;
    }
    public override void Attack(Transform target)
    {
        if (proj == null)
            proj = Instantiate(projectile, transform, false);
        proj.transform.position = Vector3.Slerp(transform.position, target.position, 1.2f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            target = other.transform;
            Attack(target);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Attack(target);
    }

}
