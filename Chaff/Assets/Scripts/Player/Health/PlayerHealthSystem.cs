using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public int playerHealth;
    [SerializeField] List<HeartBehavior> healthListing;
    [SerializeField] GameObject heartPrefab;

    private int maxHealth;
    private int healthRemaining;

    private void Awake()
    {
        maxHealth = playerHealth;
        SetHealthUI();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            PlayerTakeDamage(5);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerHeal(5);
        }
    }


    public void PlayerTakeDamage(int damage)
    {
        playerHealth -= damage;
        healthRemaining = damage;

        for(int i = healthListing.Count - 1; i > -1; i--)
        {
            if (healthRemaining <= 0)
            {
                return;
            }
            if (healthListing[i].heartValue <= 0)
            {
                healthListing[i].heartValue = 0;
                continue;
            }
            int tempHeartStore = healthListing[i].heartValue;

            healthListing[i].heartValue -= healthRemaining;
            if (healthListing[i].heartValue <= 0)
            {
                healthListing[i].heartValue = 0;
            }
            Debug.Log(healthListing[i].heartValue);
            healthListing[i].UpdateHeartUI(healthListing[i].heartValue);
            healthRemaining -= tempHeartStore;
            if (playerHealth < 0)
            {
                playerHealth = 0;
            }
        }
    }
    public void PlayerHeal(int health)
    {
        playerHealth += health;
        healthRemaining = health;
        if (playerHealth >= maxHealth || healthRemaining >= maxHealth)
        {
            playerHealth = maxHealth;
            foreach(var heart in healthListing)
            {
                heart.heartValue = 20;
                heart.UpdateHeartUI(heart.heartValue);
            }
            return;
        }

        foreach (var heart in healthListing)
        {
            if (healthRemaining <= 0)
            {
                return;
            }
            if (heart.heartValue >= 20)
            {
                heart.heartValue = 20;
                continue;
            }

            heart.heartValue += healthRemaining;
            if (heart.heartValue >= 20)
            {
                heart.heartValue = 20;
            }
            Debug.Log(heart.heartValue);
            heart.UpdateHeartUI(heart.heartValue);
            healthRemaining -= heart.heartValue;
        }
    }

    private void SetHealthUI()
    {
        int currentHealth = Mathf.RoundToInt(maxHealth / 20);
        if(currentHealth > healthListing.Count)
        {
            int tempStore = healthListing.Count;

            for (int i = 0; i < currentHealth - tempStore; i++)
            {
                GameObject heart = Instantiate(heartPrefab);
                heart.transform.SetParent(healthListing[1].transform.parent);
                healthListing.Add(heart.GetComponent<HeartBehavior>());
            }
            SetHealthUI();
        }
        else
        {
            int tempHealthStore = maxHealth;
            for (int i = 0; i < healthListing.Count; i++)
            {
                if(tempHealthStore >= 20)
                {
                    healthListing[i].heartValue = 20;
                    tempHealthStore -= 20;
                }
                else if(tempHealthStore > 0)
                {
                    healthListing[i].heartValue = tempHealthStore;
                    return;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
