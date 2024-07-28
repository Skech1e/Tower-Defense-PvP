using UnityEngine;
using UnityEngine.AI;
using static Logger;

public class Agile : Bot
{
    #region inherent vars
    [field: SerializeField]
    public override int HP { get; set; }
    [field: SerializeField]
    public override int DMG { get; set; }

    public override Transform target { get; set; }
    public override Transform enemy { get; set; }
    #endregion

    NavMeshAgent agent;
    Animator anim;

    readonly int idle = Animator.StringToHash(nameof(AnimStates.Idle));
    readonly int move = Animator.StringToHash(nameof(AnimStates.Move));
    readonly int attack = Animator.StringToHash(nameof(AnimStates.Attack));
    readonly int dead = Animator.StringToHash(nameof(AnimStates.Dead));

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        Move();
    }

    public override void Attack(Transform enemy)
    {
        transform.LookAt(enemy);
        float distance = Vector3.Distance(enemy.transform.position, transform.position);
        if (distance < 1.5f)
        {
            agent.SetDestination(transform.position);
            anim.SetBool(move, false);
            anim.SetBool(attack, true);
        }        
    }

    public override void Move()
    {
        //transform.LookAt(target);
        L.Log(target);
        agent.SetDestination(target.position);
        anim.SetBool(move, true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemy == null && other.CompareTag(Tags.Enemy.ToString()))
        {
            print(other.name);
            enemy = other.transform;
            Attack(enemy);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Enemy.ToString()))
        {
            if (other.transform == enemy)
            {
                enemy = null;
                Move();
            }
        }
    }
}
