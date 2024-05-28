using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] private bool despawnOnCollision = true;
    [SerializeField] private bool despawnOnTimer = true;
    [SerializeField] private int despawnTimer = 3;

    public float projectileSpeed = 1.5f;

    private void Update()
    {
        ProjectileMove();
        if(despawnOnTimer)
        {
            Destroy(gameObject, 3);
        }
    }

    private void ProjectileMove()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (despawnOnCollision)
        {
            Destroy(gameObject);
        }
    }
}
