using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTooltipData : MonoBehaviour
{
    public string tooltipName;
    public string tooltipTag;

    [Multiline()] 
    public string tooltipDescription;

    private void OnMouseEnter()
    {
        HoverPreviewBehavior hoverRef = FindFirstObjectByType<HoverManager>().hoverReference;

        HoverManager.ShowTooltip();
        if (GetComponent<CropPlot>() != null)
        {
            if(GetComponent<CropPlot>().plotUsed)
            {
                HoverManager.HideTooltip();
                hoverRef.DisableTooltip();
                return;
            }
        }
        if (GetComponent<FarmObject>() != null)
        {
            FarmObject objectReference = GetComponent<FarmObject>();
            if(objectReference.harvestable)
            {
                hoverRef.EnableTooltip(tooltipName, tooltipDescription + "\nMax Growth Stage: " + objectReference.maxGrowthLevel + ", Growth Chance: " + objectReference.growthChance + "\n<b>This crop is ready to be harvested.</b>", tooltipTag);
            }
            else
            {
                hoverRef.EnableTooltip(tooltipName, tooltipDescription + "\nMax Growth Stage: " + objectReference.maxGrowthLevel + ", Growth Chance: " + objectReference.growthChance, tooltipTag);
            }
        }
        else
        {
            hoverRef.EnableTooltip(tooltipName, tooltipDescription, tooltipTag);
        }

    }

    private void OnMouseExit()
    {
        HoverPreviewBehavior hoverRef = FindFirstObjectByType<HoverManager>().hoverReference;

        HoverManager.HideTooltip();
        hoverRef.DisableTooltip();
    }

}
