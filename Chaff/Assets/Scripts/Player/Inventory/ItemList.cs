using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public Item[] items;

    public Item FindItem(int itemID)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(itemID == items[i].itemNumberID)
            {
                return items[i];
            }
        }
        return null;
    }
}
