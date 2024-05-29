using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject firepoint;
    [SerializeField] float bulletSpeed = 1;
    [SerializeField] int damage = 1;
    [SerializeField] bool automatic = true;

    private float lastTimeShot = 0;
    public float firingSpeed = 0.5f;

    public void Update()
    {
        if(!automatic)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Shoot();
            }
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
                GameObject projectile = GameObject.Instantiate(bullet, firepoint.transform.position, firepoint.transform.rotation);
                projectile.transform.LookAt(hit.point);
                projectile.GetComponent<ProjectileBehavior>().projectileSpeed = bulletSpeed;
                projectile.GetComponent<ProjectileBehavior>().damage = damage;
            }
        }
    }

}