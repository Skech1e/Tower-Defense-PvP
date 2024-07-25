using UnityEngine;

public class Agile : Bot
{
    [field: SerializeField]
    public override int HP { get; set; }
    [field: SerializeField]
    public override int DMG { get; set; }

    public override Transform target { get; set; }
    Animator anim;

    readonly int idle = Animator.StringToHash(nameof(AnimStates.Idle));
    readonly int move = Animator.StringToHash(nameof(AnimStates.Move));
    readonly int attack = Animator.StringToHash(nameof(AnimStates.Attack));
    readonly int dead = Animator.StringToHash(nameof(AnimStates.Dead));

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        anim.SetBool(idle, true);
    }

    public override void Attack(Transform enemy)
    {
        transform.LookAt(enemy);
        float distance = Vector3.Distance(enemy.transform.position, transform.position);
        if (distance < 1.5f)
        {
            anim.SetBool(move, false);
            anim.SetBool(attack, true);
        }
    }
    public override void Move()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tags.Enemy.ToString()))
        {
            print(other.name);
            Attack(other.transform);
        }
    }
}
