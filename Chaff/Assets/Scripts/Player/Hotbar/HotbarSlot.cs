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

    private int tempAmmo = 0;

    public void GetAmmo()
    {
        tempAmmo = FindFirstObjectByType<GunSystem>().oneTimeAmmo;
    }

    public void RestoreAmmo()
    {
        if (tempAmmo > 0)
        {
            FindFirstObjectByType<GunSystem>().oneTimeAmmo = tempAmmo;
        }
    }

}
