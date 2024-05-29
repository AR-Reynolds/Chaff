using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [System.Serializable]
    public class ItemInfo
    {
        public string inventory_itemID = null;
        public int inventory_quantity = 0;
        public int inventory_itemNumberID = 0;
        public bool inventory_equipped = false;
        public InventoryTag inventoryTag;
    }

    public List<ItemInfo> playerInventory;

    private ItemList itemIndex;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        itemIndex = FindFirstObjectByType<ItemList>();
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

        if(playerInventory.Count > 0 && item != null)
        {
            foreach (ItemInfo itemInfo in playerInventory)
            {
                ItemInfo itemtoFind = playerInventory.Find((i) => i.inventory_itemID == item.itemID);

                itemtoFind.inventory_quantity += amountToAdd;
            }
        }
        else
        {
            ItemInfo newItem = new ItemInfo();
            newItem.inventory_itemID = item.itemID;
            newItem.inventory_itemNumberID = item.itemNumberID;
            newItem.inventoryTag = item.inventoryTag;

            playerInventory.Add(newItem);
        }
    }
    public void RemovefromInventory(int itemID, int amountToRemove)
    {
        Item item = itemIndex.FindItem(itemID);

        if (playerInventory.Count > 0 && item != null)
        {
            Debug.Log("This works");
            ItemInfo itemtoFind = playerInventory.Find((i) => i.inventory_itemID == item.itemID);

            Debug.Log("Found");
            itemtoFind.inventory_quantity -= amountToRemove;
            if (itemtoFind.inventory_quantity <= 0)
            {
                playerInventory.Remove(itemtoFind);
            }
        }
        else
        {
            Debug.Log("Could not find!");
        }
    }

}