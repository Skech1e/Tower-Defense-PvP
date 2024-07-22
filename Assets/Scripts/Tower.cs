using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public abstract int HP { get; set; }
    public abstract int DMG { get; set; }
    public abstract Transform target { get; set; }
    public abstract GameObject projectile { get; set; }
    public abstract void Attack(Transform target);
}
