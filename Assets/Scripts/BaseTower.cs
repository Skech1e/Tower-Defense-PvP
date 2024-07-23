using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BaseTower : Tower
{
    [field: SerializeField]
    public override byte HP { get; set; }
    [field: SerializeField]
    public override byte DMG { get; set; }
    [field: SerializeField, Range(0.1f, 10f)]
    public override float Range { get; set; }
    [field: SerializeField]
    public override Transform target { get; set; }
    [field: SerializeField]
    public override Projectile projectile { get; set; }
    public override Transform pLaunchPos { get; set; }

    Projectile pball;
    public bool canShoot;

    [Range(0f, 5f)] public float speed;

    SphereCollider Aoe;
    private void Awake()
    {
        Aoe = GetComponent<SphereCollider>();
        Aoe.isTrigger = true;
        Aoe.radius = Range;
        pLaunchPos = transform.GetChild(0);
    }
    public override void Attack(Transform target)
    {
        pball = Instantiate(projectile, pLaunchPos, false);
        pball.speed = speed;
        pball.target = target;
        pball.dmg = DMG;
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Enemy") && pball == null)
        {
            target = other.transform;
            Attack(target);
        }
    }

    private void FixedUpdate()
    {
        
    }

    bool SmoothMove(Vector3 from, Vector3 to, Transform t)
    {
        speed += Time.deltaTime;
        while (t.position != to)
        {
            t.position = Vector3.Slerp(from, to, speed);
            canShoot = false;
            return canShoot;
        }
        t.localPosition = Vector3.zero;
        canShoot = true;
        return canShoot;
    }
}
