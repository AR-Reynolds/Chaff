using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using static PlayerInventory;
using static UnityEditor.Progress;

public class HotbarManager : MonoBehaviour
{
    Player player;
    PlayerInventory inventory;
    [SerializeField] GameObject equipObject;
    public List<HotbarSlot> hotbarSlots;

    private void Awake()
    {
        player = GetComponent<Player>();
        inventory = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (hotbarSlots[0].containsItem)
            {
                UnequipPhysicalItem();
                foreach (var slot in hotbarSlots)
                {
                    slot.equipped = false;
                }

                EquipPhysicalItem(hotbarSlots[0]);
                hotbarSlots[0].equipped = true;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (hotbarSlots[1].containsItem)
            {
                UnequipPhysicalItem();
                foreach (var slot in hotbarSlots)
                {
                    slot.equipped = false;
                }

                EquipPhysicalItem(hotbarSlots[1]);
                hotbarSlots[1].equipped = true;
            }
            return;
        }
    }

    public void EquipPhysicalItem(HotbarSlot selectedSlot)
    {
        GameObject item = Instantiate(selectedSlot.itemReference.physicalReference, equipObject.transform.position, equipObject.transform.rotation);
        item.transform.parent = equipObject.transform;
    }
    public void UnequipPhysicalItem()
    {
        if (equipObject.transform.childCount == 0)
        {
            return;
        }
        for(int i = 0; i < equipObject.transform.childCount; i++)
        {
            Destroy(equipObject.transform.GetChild(i).gameObject);
        }
    }

    public void AssignToHotbar(Item item)
    {
        foreach(var slot in hotbarSlots)
        {
            if(slot.containsItem || item.inventoryTag != slot.hotbarTag)
            {
                continue;
            }
            slot.containsItem = true;
            slot.equipped = false;
            slot.hotbarItemName = item.itemName;
            slot.itemReference = item;
            return;
        }
    }
    public void RemoveFromHotbar(Item item)
    {
        foreach (var slot in hotbarSlots)
        {
            HotbarSlot slotWithData = hotbarSlots.Find((i) => i.itemReference == item);

            if(slotWithData != null)
            {
                slotWithData.containsItem = false;
                slotWithData.equipped = false;
                slotWithData.hotbarIcon = null;
                slotWithData.hotbarItemName = "";
                slotWithData.itemReference = null;
            }
        }
    }
}
