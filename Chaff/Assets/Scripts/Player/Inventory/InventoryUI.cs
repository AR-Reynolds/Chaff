using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    Player player;
    PlayerInventory inventory;
    public List<PlayerInventorySlot> inventorySlots;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>();
        inventory = FindFirstObjectByType<PlayerInventory>();
    }

    public void UpdateInventoryUI()
    {
        if (inventory.playerInventory.Count >= 1)
        {
            int slotCount = 0;
            foreach (PlayerInventorySlot slot in inventorySlots)
            {
                slot.UpdateText("");

                if (slotCount < inventory.playerInventory.Count)
                {
                    slot.itemIDReference = inventory.playerInventory[slotCount].inventory_itemNumberID;
                    slot.itemQuantity = inventory.playerInventory[slotCount].inventory_quantity;
                    slot.itemReference = inventory.itemIndex.FindItem(slot.itemIDReference);
                    slot.inventoryTag = inventory.playerInventory[slotCount].inventoryTag;
                    slot.inventoryIcon = inventory.playerInventory[slotCount].inventoryIcon;
                    slot.UpdateText(slot.itemReference.itemName);
                    slotCount++;
                }
                else
                {
                    slot.UpdateText("");
                    slot.ResetSlot();
                    slotCount++;
                }
                if (slotCount >= inventorySlots.Count)
                {
                    slotCount = 0;
                    return;
                }
            }
        }
    }
}
