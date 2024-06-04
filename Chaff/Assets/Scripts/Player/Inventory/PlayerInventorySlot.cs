using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string itemWordReference = "";
    public int itemIDReference = 0;
    public int itemQuantity = 0;
    public Item itemReference = null;
    public Image inventoryIcon = null;
    public InventoryTag inventoryTag;

    private bool hover = false;

    public void UpdateText(string name)
    {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = name + " x" + itemQuantity;
    }
    public void ResetSlot()
    {
    itemQuantity = 0;
    itemWordReference = "";
    itemIDReference = 0;
    itemReference = null;
    inventoryIcon = null;
    inventoryTag = InventoryTag.Material;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverPreviewBehavior hoverRef = FindFirstObjectByType<HoverManager>().hoverReference;

        Debug.Log("Mouse enter");
        if(itemReference != null)
        {
            HoverManager.ShowTooltip();
            hoverRef.EnableTooltip(itemReference);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverPreviewBehavior hoverRef = FindFirstObjectByType<HoverManager>().hoverReference;

        Debug.Log("Mouse exit");
        hoverRef.DisableTooltip();
        HoverManager.HideTooltip();
    }
}
