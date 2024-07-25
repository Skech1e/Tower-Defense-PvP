using UnityEngine;

public abstract class Bot : MonoBehaviour
{
    public abstract int HP { get; set; }
    public abstract int DMG { get; set; }

    public abstract Transform target { get; set; }

    public abstract void Attack(Transform enemy);
    public abstract void Move();
}
