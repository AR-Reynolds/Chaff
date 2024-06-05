using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerReference : MonoBehaviour
{
    public FarmManager farmManager;
    public ItemList itemIndex;
    public PlayerInventory inventory;
    
    private void Awake()
    {
        farmManager = FindFirstObjectByType<FarmManager>();
        itemIndex = FindFirstObjectByType<ItemList>();
        inventory = FindFirstObjectByType<PlayerInventory>();

        DontDestroyOnLoad(gameObject);
    }
}
