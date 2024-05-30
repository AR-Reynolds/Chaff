using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropPlot : MonoBehaviour
{
    public bool plotUsed = false;
    public Transform plantPos;

    FarmManager farmManager;

    public void Awake()
    {
        farmManager = FindFirstObjectByType<FarmManager>();
    }

    public void Crop(Button button)
    {
        if (plantPos.transform.childCount == 0)
        {
            plotUsed = false;
        }
        if (plotUsed)
        {
            Debug.Log("crop");
            return;
        }
        PlayerInventory inv = FindFirstObjectByType<PlayerInventory>();

        FarmObject selection = farmManager.referenceCrops.Find(i => i.cropNameID == button.GetComponent<CropSelectionSlot>().cropName);
        if (selection != null)
        {
            if (inv.FindInventoryItem(selection.plantSeed.itemNumberID) == null)
            {
                return;
            }
            inv.RemovefromInventory(selection.plantSeed.itemNumberID, 1);
            farmManager.PlantCrop(selection.gameObject, plantPos.transform.position, plantPos.gameObject);
            plotUsed = true;
        }
    }


}
