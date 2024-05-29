using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera;

public class PlayerCropManager : MonoBehaviour
{
    FarmManager farmManager;
    Player player;
    [SerializeField] GameObject selected;
    [SerializeField] private Canvas cropSelectionCanvas;

    private void Awake()
    {
        farmManager = FindFirstObjectByType<FarmManager>();
        player = FindFirstObjectByType<Player>();

        for (int i = 0; i < cropSelectionCanvas.GetComponentInChildren<GridLayoutGroup>().gameObject.transform.childCount; i++)
        {
            GameObject button = cropSelectionCanvas.GetComponentInChildren<GridLayoutGroup>().gameObject.transform.GetChild(i).gameObject;
            if(button != null)
            {
                if(i >= farmManager.referenceCrops.Count)
                {
                    return;
                }
                button.GetComponentInChildren<TextMeshProUGUI>().text = farmManager.referenceCrops[i].name;
                button.GetComponent<CropSelectionSlot>().cropName = farmManager.referenceCrops[i].cropNameID;
            }
        }
    }

    public void SelectCrop(GameObject button)
    {
        player.cropPlot.GetComponent<CropPlot>().Crop(button.GetComponent<Button>());
    }

}
