using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public int playerHealth;
    [SerializeField] Image health;

    private int maxHealth;

    private void Awake()
    {
        maxHealth = playerHealth;
    }


    public void PlayerTakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth < 0)
        {
            playerHealth = 0;
        }
    }
}
