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
        farmManager = GetComponent<FarmManager>();
    }

    public void Crop(Button button)
    {
        if(plotUsed)
        {
            Debug.Log("crop");
            return;
        }

        FarmObject selection = farmManager.referenceCrops.Find(i => i.cropNameID == button.GetComponent<CropSelectionSlot>().cropName);
        if (selection != null)
        {
            farmManager.PlantCrop(selection.gameObject, plantPos.transform.position);
            plotUsed = true;
        }
    }


}
