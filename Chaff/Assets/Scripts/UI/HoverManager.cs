using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    [SerializeField] public HoverPreviewBehavior hoverReference;
    private static HoverManager manager;

    private bool fade = false;

    private void Awake()
    {
        manager = this;
    }

    public static void ShowTooltip()
    {
        manager.StopAllCoroutines();
        manager.hoverReference.gameObject.SetActive(true);
        manager.fade = true;
    }
    public static void HideTooltip()
    {
        manager.StopAllCoroutines();
        manager.StartCoroutine(manager.Fade());
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(1);
        if (manager.fade)
        {
            manager.hoverReference.gameObject.SetActive(false);
        }
        manager.fade = false;
    }
}
