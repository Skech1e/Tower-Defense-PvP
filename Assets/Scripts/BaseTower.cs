using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
public class BaseTower : Tower
{
    [field: SerializeField] public E_Player player { get; private set; }

    #region Inherent vars
    [field: SerializeField] public override byte HP { get; set; }
    [field: SerializeField] public override byte DMG { get; set; }
    [field: SerializeField] public override Projectile projectile { get; set; }
    [field: SerializeField, Range(0.1f, 10f)] public override float Range { get; set; }
    public override Transform shooter { get; set; }
    #endregion

    [SerializeField] Transform spawnPoint;

    Projectile orb;
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


    /*public IEnumerator WaitforRouteSelect(int type)
    {        
        Transform route = target;
        float timer = timeForRouteSelect;
        yield return new WaitUntil(() => {
            HighlightTowers();
            timer -= Time.deltaTime; 
            return route != null || timer <= 0; 
        } );
        routeMat.SetColor("_EmissionColor", Color.black);
        Spawn(spawnType[type], route);
    }
    

    void HighlightTowers()
    {
        Color color = Color.blue;
        float emission = Mathf.PingPong(Time.time, 1f);
        Color final = color * Mathf.LinearToGammaSpace(emission);
        routeMat.SetColor("_EmissionColor", final);
    }*/

    public void Spawn(Bot bot, Transform route)
    {
        NavMeshAgent b = Instantiate(bot, spawnPoint.position, Quaternion.identity).GetComponent<NavMeshAgent>();
        b.SetDestination(route.position);
        /*if (b is Agile)
        {
            var ab = (Agile)b;
            ab.target = route;
        }
        else
        {
            var ab = (Heavy)b;
            ab.target = route;
        }*/        
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Enemy") && orb == null)
        {
            Attack(other.transform);
        }
    }

}
