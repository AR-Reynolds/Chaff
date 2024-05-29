using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class UtilitySystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject firepoint;
    [SerializeField] float projectileSpeed = 1;
    [SerializeField] int damage = 1;

    [Header("Type of Utility")]
    [SerializeField] bool oneTimeUse = true;

    private float lastTimeShot = 0;
    public float firingSpeed = 0.5f;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(cursorRay, out hit))
        {
            if(lastTimeShot + firingSpeed <= Time.time)
            {
                lastTimeShot = Time.time;
                GameObject utilProjectile = GameObject.Instantiate(projectile, firepoint.transform.position, firepoint.transform.rotation);
                utilProjectile.transform.LookAt(hit.point);
                utilProjectile.GetComponent<ProjectileBehavior>().projectileSpeed = projectileSpeed;
                utilProjectile.GetComponent<ProjectileBehavior>().damage = damage;
            }
        }
        if(oneTimeUse)
        {
            Destroy(gameObject);
        }
    }

}