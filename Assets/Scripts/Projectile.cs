using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    public byte dmg;
    [SerializeField, Range(1f, 5f)]float timeAlive;
    
    private void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        timeAlive -= Time.deltaTime;
        if(timeAlive < 0.1f)
            Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(nameof(Tags.Enemy)))
        {
            Destroy(gameObject);
        }
    }
}
