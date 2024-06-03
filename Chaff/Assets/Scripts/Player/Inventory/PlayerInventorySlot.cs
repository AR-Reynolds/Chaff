using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventorySlot : MonoBehaviour
{
    public string itemWordReference = "";
    public int itemIDReference = 0;
    public int itemQuantity = 0;
    public Item itemReference = null;
    public Image inventoryIcon = null;
    public InventoryTag inventoryTag;

    public void UpdateText(string name)
    {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = name + " x" + itemQuantity;
    }
    public void ResetSlot()
    {
    itemWordReference = "";
    itemIDReference = 0;
    itemQuantity = 0;
    itemReference = null;
    inventoryIcon = null;
    inventoryTag = InventoryTag.Material;
    }
}
