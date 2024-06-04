using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    [SerializeField] public HoverPreviewBehavior hoverReference;
    private static HoverManager manager;

    private void Awake()
    {
        manager = this;
    }

    public static void ShowTooltip()
    {
        manager.hoverReference.gameObject.SetActive(true);
    }
    public static void HideTooltip()
    {
        manager.hoverReference.gameObject.SetActive(false);
    }
}
