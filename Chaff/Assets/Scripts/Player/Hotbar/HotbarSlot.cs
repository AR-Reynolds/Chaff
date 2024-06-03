using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HotbarSlot : MonoBehaviour
{
    public string hotbarItemName = "";
    public bool containsItem = false;
    public bool equipped = false;
    public Item itemReference = null;
    public Image hotbarIcon = null;
    public InventoryTag hotbarTag;
}
