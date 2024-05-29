using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health Settings")]
    [Min(1), SerializeField] public int enemyHealth = 100;
    [SerializeField] private float enemyRegenTick = 0.05f;
    [SerializeField] private int enemyRegenDelay = 5;

    [Header("Enemy Shield Settings")]
    [SerializeField] public bool enableShield = true;
    [Min(1), SerializeField] public int enemyShield = 100;
    [SerializeField] private float enemyShieldRegenTick = 0.05f;
    [SerializeField] private int enemyShieldRegenDelay = 5;

    private int enemy_maxHealth;
    private int enemy_maxShield;

    private bool enemyAlive = true;
    private bool enemy_shieldRegen = false;
    private bool enemy_regenActive = false;

    private string killerName = "Herobrine";
    private string weaponName = "gun";
    private void Awake()
    {
        enemy_maxHealth = enemyHealth;
        if (enableShield)
        {
            enemy_maxShield = enemyShield;
        }
        else
        {
            enemy_maxShield = 0;
            enemyShield = 0;
        }
    }
    private void Update()
    {
        if (enableShield)
        {
            EnemyShieldRegen();
        }
        EnemyHealthRegen();
        EnemyHealthFix();
        Die();
    }
    // external functionality
    public void EnemyDamage(int damage)
    {
        if (!enemyAlive) { return; }
        if (enemyShield > 0)
        {
            int damagetoSubtract = enemyShield;
            enemyShield -= damage;
            damage -= damagetoSubtract;
            enemy_shieldRegen = false;
            if (damage > 0)
            {
                enemyHealth -= damage;
                enemy_regenActive = false;
            }
        }
        else
        {
            enemyHealth -= damage;
            enemy_regenActive = false;
        }
    }
    public void EnemyHeal(int health)
    {
        if (!enemyAlive || enemyHealth == enemy_maxHealth) { return; }
        enemyHealth += health;
        if (enemyHealth > enemy_maxHealth)
        {
            enemyHealth = enemy_maxHealth;
        }
    }

    // natural regen
    private void EnemyHealthRegen()
    {
        if (!enemyAlive || enemyShield >= enemy_maxHealth) { return; }

        StartCoroutine(EnemyRegen());
    }
    private void EnemyShieldRegen()
    {
        if (!enemyAlive || enemyShield >= enemy_maxHealth) { return; }

        StartCoroutine(EnemyShield());
    }
    private IEnumerator EnemyRegen()
    {
        if (!enemy_regenActive)
        {
            yield return new WaitForSeconds(enemyRegenDelay);
            enemy_regenActive = true;
        }

        yield return new WaitForSeconds(enemyRegenTick);
        enemyHealth++;
        StopAllCoroutines();
    }
    private IEnumerator EnemyShield()
    {
        if (!enemy_shieldRegen)
        {
            yield return new WaitForSeconds(enemyShieldRegenDelay);
            enemy_shieldRegen = true;
        }

        yield return new WaitForSeconds(enemyShieldRegenTick);
        enemyShield++;
        StopAllCoroutines();
    }
    // internal functionality
    private void EnemyHealthFix()
    {
        if (enemyHealth < 0)
        {
            enemyHealth = 0;
        }
        if (enemyShield < 0)
        {
            enemyShield = 0;
        }
        if (enemyHealth > enemy_maxHealth)
        {
            enemyHealth = enemy_maxHealth;
        }
        if (enemyShield > enemy_maxShield)
        {
            enemyShield = enemy_maxShield;
        }
    }
    private void Die()
    {
        if (enemyHealth <= 0)
        {
            enemyAlive = false;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (collision.gameObject.GetComponent<ProjectileBehavior>() != null)
            {
                EnemyDamage(collision.gameObject.GetComponent<ProjectileBehavior>().damage);
            }
        }
    }
}