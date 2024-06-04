using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.Loading;

public class HoverPreviewBehavior : MonoBehaviour
{
    public string tooltipName = "Item";
    public string tooltipDescription = "grungy";
    public InventoryTag tooltipTag = InventoryTag.Material;
    public RectTransform recTransform;

    Image image;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] LayoutElement layoutElement;
    [SerializeField] int characterWrapLimit;

    private void Awake()
    {
        image = GetComponent<Image>();
        recTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        int titleLength = titleText.text.Length;
        int descriptionLength = descriptionText.text.Length;
        layoutElement.enabled = (titleLength > characterWrapLimit || descriptionLength > characterWrapLimit) ? true : false;

        if (gameObject.activeSelf)
        {
            Vector2 position = Input.mousePosition;
            transform.position = position;

            float pivotX = position.x / Screen.width;
            float pivotY = position.y / Screen.height;

            recTransform.pivot = new Vector2(pivotX, pivotY + 1);
        }
    }

    public void EnableTooltip(Item item)
    {
        gameObject.SetActive(true);
        tooltipName = item.itemName;
        tooltipDescription = item.description;
        tooltipTag = item.inventoryTag;

        titleText.text = tooltipName;
        descriptionText.text = tooltipDescription;
        transform.position = Input.mousePosition;
    }

    public void DisableTooltip()
    {
        gameObject.SetActive(false);
        tooltipName = null;
        tooltipDescription = null;
        tooltipTag = InventoryTag.Material;
    }
}
