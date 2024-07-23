using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public abstract byte HP { get; set; }
    public abstract byte DMG { get; set; }
    public abstract float Range { get; set; }
    public abstract Transform target { get; set; }
    public abstract Projectile projectile { get; set; }
    public abstract Transform pLaunchPos { get; set; }
    public abstract void Attack(Transform target);
}
