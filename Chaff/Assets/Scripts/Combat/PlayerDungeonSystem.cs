using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerDungeonSystem : MonoBehaviour
{
    [System.Serializable]
    public class Glooby
    {
        public Item itemReference;
        public InventoryTag tag;
        public int quantity;
        public int ammoCount;
        public bool equipped;
    }

    [SerializeField] List<Glooby> gloobies;

    private int maxAmount = 3;

    public void DungeonAssignToList(Item item)
    {
        if(item.inventoryTag == InventoryTag.Weapon || item.inventoryTag == InventoryTag.Utility)
        {
            if (gloobies.Count > 0)
            {
                Glooby itemToFind = gloobies.Find(i => i.itemReference == item);
                if (itemToFind != null && itemToFind.quantity < maxAmount)
                {
                    itemToFind.quantity++;
                    FindFirstObjectByType<PlayerInventory>().RemovefromInventory(item.itemNumberID, 1);
                }
                else if (itemToFind == null)
                {
                    Glooby newItem = new Glooby
                    {
                        itemReference = item,
                        tag = item.inventoryTag,
                        quantity = 1,
                        ammoCount = 0,
                        equipped = false
                    };
                    gloobies.Add(newItem);
                    FindFirstObjectByType<PlayerInventory>().RemovefromInventory(item.itemNumberID, 1);
                }
            }
            else
            {
                if (gloobies.Count < 7)
                {
                    Glooby newItem = new Glooby
                    {
                        itemReference = item,
                        tag = item.inventoryTag,
                        quantity = 1,
                        ammoCount = 0,
                        equipped = false
                    };
                    gloobies.Add(newItem);
                    FindFirstObjectByType<PlayerInventory>().RemovefromInventory(item.itemNumberID, 1);
                }
            }
        }
    }

    public void DungeonRemoveFromList(Item item)
    {
        if (gloobies.Count > 0)
        {
            Glooby itemToFind = gloobies.Find(i => i.itemReference == item);

            if (itemToFind != null && itemToFind.quantity > 0)
            {
                itemToFind.quantity--;
            }
            if(itemToFind.quantity <= 0)
            {
                gloobies.Remove(itemToFind);
            }
        }
    }

}
