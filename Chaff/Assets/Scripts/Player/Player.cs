using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject placetoInstantiate;

    [SerializeField] public Canvas cropSelectionCanvas;
    public GameObject cropPlot;

    private void Awake()
    {
        cropSelectionCanvas.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlantTrigger")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                FindFirstObjectByType<FarmManager>().Harvest(other.gameObject.transform.parent.gameObject);
            }
        }
        else if (other.gameObject.tag == "CropPlot")
        {
            if (!other.gameObject.GetComponent<CropPlot>().plotUsed)
            {
                cropSelectionCanvas.enabled = true;
                cropPlot = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CropPlot")
        {
            cropSelectionCanvas.enabled = false;
            cropPlot = null;
        }
    }
}
