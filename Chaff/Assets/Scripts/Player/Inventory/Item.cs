using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InventoryTag { Material, Weapon, Utility, Equipment };

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string itemName = "Item";
    public string description = "Description";
    public string itemID = null;
    public int itemNumberID = 0;
    public InventoryTag inventoryTag;
    public GameObject physicalReference;
}
