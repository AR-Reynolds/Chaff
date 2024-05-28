using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool despawnOnCollision = true;
    [SerializeField] private bool despawnOnTimer = true;
    [SerializeField] private int despawnTimer = 3;

    [Header("Projectile Type")]
    [SerializeField] private bool spawnDamageZone = false;
    [SerializeField] private GameObject damageZone;
    [SerializeField] private float zoneDespawnTimer;

    public float projectileSpeed = 1.5f;
    public int damage = 1;

    private void Update()
    {
        ProjectileMove();
        if(despawnOnTimer)
        {
            Destroy(gameObject, despawnTimer);
        }
    }

    private void ProjectileMove()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (spawnDamageZone)
        {
            GameObject zone = Instantiate(damageZone, transform.position, Quaternion.identity);
            Destroy(zone, zoneDespawnTimer);
        }
        if (despawnOnCollision)
        {
            Destroy(gameObject);
        }
    }
}
