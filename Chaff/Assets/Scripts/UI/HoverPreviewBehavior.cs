using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.Loading;
using static UnityEditor.Progress;
using DG.Tweening;
using Unity.VisualScripting;

public class HoverPreviewBehavior : MonoBehaviour
{
    [Header("Tooltip Stuff")]
    public string tooltipName = "Item";
    public string tooltipDescription = "grungy";
    public InventoryTag tooltipTag = InventoryTag.Material;

    [Header("References")]
    public RectTransform recTransform;
    Image image;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI tagText;
    [SerializeField] LayoutElement layoutElement;
    [SerializeField] int characterWrapLimit;

    private Tween tooltipFade;
    private RectTransform canvasTransform;

    private void Awake()
    {
        tooltipFade = DOTween.Sequence();
        image = GetComponent<Image>();
        recTransform = GetComponent<RectTransform>();
        canvasTransform = transform.parent.GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        int titleLength = titleText.text.Length;
        int descriptionLength = descriptionText.text.Length;
        layoutElement.enabled = (titleLength > characterWrapLimit || descriptionLength > characterWrapLimit) ? true : false;

        if (gameObject.activeSelf)
        {
            Vector2 anchor = recTransform.anchoredPosition = Input.mousePosition / canvasTransform.localScale.x;
            if(anchor.x + recTransform.rect.width > canvasTransform.rect.width)
            {
                anchor.x = canvasTransform.rect.width - recTransform.rect.width;
            }
            if (anchor.y + recTransform.rect.height > canvasTransform.rect.height)
            {
                anchor.y = canvasTransform.rect.height - recTransform.rect.height;
            }

            recTransform.anchoredPosition = anchor;
        }
    }

    public void EnableTooltip(string name, string description, string tag)
    {
        GetComponent<Image>().color = new Color32(31, 31, 31, 0);
        gameObject.SetActive(true);
        Fade(0.8f, 0.1f, () => { });
        tooltipName = name;
        tooltipDescription = description;

        titleText.text = tooltipName;
        descriptionText.text = tooltipDescription;
        tagText.text = tag;
        transform.position = Input.mousePosition;
    }

    public void DisableTooltip()
    {
        Fade(0, 0.1f, () => { });
        tooltipName = null;
        tooltipDescription = null;
        tooltipTag = InventoryTag.Material;
    }


    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (tooltipFade != null)
        {
            tooltipFade.Kill(false);
        }

        tooltipFade = GetComponent<Image>().DOFade(endValue, duration);
        titleText.DOFade(endValue, duration);
        descriptionText.DOFade(endValue, duration);
        tagText.DOFade(endValue, duration);
        tooltipFade.onComplete += onEnd;
    }

    private IEnumerator Fade()
    {
        Fade(0, 0.1f, () => { });
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
