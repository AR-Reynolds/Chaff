using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyScript : MonoBehaviour
{
    [Header("Navigation Settings")]
    [SerializeField] float chaseDistance;
    [SerializeField] GameObject player;

    [Header("Enemy Settings")]
    [SerializeField] private bool meleeEnabled = true;
    [SerializeField] private bool rangedEnabled = false;

    [Header("Melee Attack Settings")]
    [SerializeField] private float meleeRange = 2.5f;
    [SerializeField] private float meleeCooldown = 0.5f;
    [SerializeField] private float meleeDelay = 0.5f;
    [SerializeField] private int meleeDamage = 10;
    [SerializeField] GameObject meleeHitbox;
    [SerializeField] GameObject meleeFirepoint;

    [Header("Ranged Attack Settings")]
    [SerializeField] private int rangedDamage = 10;
    [SerializeField] private int explosionDamage = 10;
    [SerializeField] private int ammoCount = 30;
    [SerializeField] private int bulletsToShoot = 1;
    [SerializeField] private float bulletSpeed = 2.5f;
    [SerializeField] private float bulletInaccuracy = 2.5f;
    [SerializeField] private float rangedAggroDistance = 2.5f;
    [SerializeField] private float rangedFirerate = 0.5f;
    [SerializeField] private float rangedCooldown = 0.5f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject rangedFirepoint;

    private bool canAttack = true;
    private int secretAmmo;
    NavMeshAgent agent;
    EnemyTooltipData tooltipData;
    Vector3 home;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        home = transform.position;
        agent = GetComponent<NavMeshAgent>();
        tooltipData = GetComponent<EnemyTooltipData>();
        secretAmmo = ammoCount;
    }
    private void Update()
    {
        if (meleeEnabled)
        {
            StartCoroutine(MeleeAttack());
        }
        else if (rangedEnabled)
        {
            RangedAttack();
        }
        Vector3 moveDirection = player.transform.position - transform.position;
        if (moveDirection.magnitude < chaseDistance)
        {
            agent.destination = player.transform.position;
        }
        else
        {
            agent.destination = home;
        }
    }
    private IEnumerator MeleeAttack()
    {
        Vector3 meleeRadius = player.transform.position - transform.position;
        Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        IEnumerator cooldown = MeleeCooldown(meleeCooldown);

        if (meleeRadius.magnitude <= meleeRange && canAttack)
        {
            gameObject.transform.LookAt(targetPos);
            agent.isStopped = true;
            if(meleeFirepoint.transform.childCount == 0)
            {
                yield return new WaitForSeconds(meleeDelay);
                GameObject melee = Instantiate(meleeHitbox, meleeFirepoint.transform.position, Quaternion.identity);
                melee.transform.parent = meleeFirepoint.transform;
                Destroy(melee, 0.25f);
                StopAllCoroutines();
                StartCoroutine(cooldown);
            }
            else
            {
                for(int i = 0; i < meleeFirepoint.transform.childCount; i++)
                {
                    Destroy(meleeFirepoint.transform.GetChild(i).gameObject);
                }
                yield return new WaitForSeconds(meleeDelay);
                GameObject melee = Instantiate(meleeHitbox, meleeFirepoint.transform.position, Quaternion.identity);
                melee.transform.parent = meleeFirepoint.transform;
                Destroy(melee, 0.25f);
                StopAllCoroutines();
                StartCoroutine(cooldown);
            }
        }
        else
        {
            agent.isStopped = false;
        }
    }
    private void RangedAttack()
    {
        Vector3 rangedRadius = player.transform.position - transform.position;
        Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y + 1, player.transform.position.z);
        IEnumerator cooldown = MeleeCooldown(rangedFirerate);
        IEnumerator reloadCooldown = ReloadTime(rangedCooldown);

        if (rangedRadius.magnitude <= rangedAggroDistance && canAttack && secretAmmo != 0)
        {
            gameObject.transform.LookAt(targetPos);
            agent.isStopped = true;
            for (int x = 0; x < bulletsToShoot; x++)
            {
                Debug.Log("gun");
            }
            StartCoroutine(cooldown);
            secretAmmo -= bulletsToShoot;
        }
        else
        {
            if (secretAmmo == 0)
            {
                StartCoroutine(reloadCooldown);
            }
            agent.isStopped = false;
        }
    }

    private IEnumerator MeleeCooldown(float seconds)
    {
        canAttack = false;
        yield return new WaitForSeconds(seconds);
        canAttack = true;
        StopAllCoroutines();
    }
    private IEnumerator ReloadTime(float seconds)
    {
        canAttack = false;
        yield return new WaitForSeconds(seconds);
        canAttack = true;
        secretAmmo = ammoCount;
        StopAllCoroutines();
    }

}