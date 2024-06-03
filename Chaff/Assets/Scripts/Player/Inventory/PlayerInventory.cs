using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    [System.Serializable]
    public class ItemInfo
    {
        public string inventory_itemID = null;
        public Image inventoryIcon = null;
        public int inventory_quantity = 0;
        public int inventory_itemNumberID = 0;
        public bool inventory_equipped = false;
        public InventoryTag inventoryTag;
    }

    public List<ItemInfo> playerInventory;

    public ItemList itemIndex;
    InventoryUI itemUI;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        itemIndex = FindFirstObjectByType<ItemList>();
        itemUI = FindFirstObjectByType<InventoryUI>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            RemovefromInventory(0, 15);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }

    public void AddtoInventory(int itemID, int amountToAdd)
    {
        Item item = itemIndex.FindItem(itemID);

        if(playerInventory.Count > 0)
        {
            if(item != null)
            {
                ItemInfo itemtoFind = playerInventory.Find(i => i.inventory_itemID == item.itemID);

                if (itemtoFind != null)
                {
                    itemtoFind.inventory_quantity += amountToAdd;
                    itemUI.UpdateInventoryUI();
                }
                else
                {
                    ItemInfo newItem = new ItemInfo();
                    newItem.inventory_itemID = item.itemID;
                    newItem.inventory_itemNumberID = item.itemNumberID;
                    newItem.inventoryTag = item.inventoryTag;
                    newItem.inventory_quantity = amountToAdd;

                    playerInventory.Add(newItem);
                    itemUI.UpdateInventoryUI();
                }
            }
        }
        else
        {
            ItemInfo newItem = new ItemInfo();
            newItem.inventory_itemID = item.itemID;
            newItem.inventory_itemNumberID = item.itemNumberID;
            newItem.inventoryTag = item.inventoryTag;
            newItem.inventory_quantity = amountToAdd;

            playerInventory.Add(newItem);
            itemUI.UpdateInventoryUI();
        }
    }
    public void RemovefromInventory(int itemID, int amountToRemove)
    {
        Item item = itemIndex.FindItem(itemID);

        if (playerInventory.Count > 0 && item != null)
        {
            ItemInfo itemtoFind = playerInventory.Find((i) => i.inventory_itemID == item.itemID);

            itemtoFind.inventory_quantity -= amountToRemove;
            if (itemtoFind.inventory_quantity <= 0)
            {
                playerInventory.Remove(itemtoFind);
                itemUI.UpdateInventoryUI();
                return;
            }
            itemUI.UpdateInventoryUI();
        }
        else
        {
            Debug.Log("Could not find!");
        }
    }

    public ItemInfo FindInventoryItem(int itemID)
    {
        Item item = itemIndex.FindItem(itemID);

        if (playerInventory.Count > 0 && item != null)
        {
            ItemInfo itemtoFind = playerInventory.Find((i) => i.inventory_itemID == item.itemID);

            if(itemtoFind != null)
            {
                return itemtoFind;
            }
            else
            {
                Debug.Log("Could not find!");
                return null;
            }
        }
        else
        {
            Debug.Log("Could not find!");
            return null;
        }
    }
}
