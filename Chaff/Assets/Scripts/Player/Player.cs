using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] GameObject utility;
    [SerializeField] GameObject placetoInstantiate;

    [SerializeField] public Canvas cropSelectionCanvas;
    public GameObject cropPlot;

    private void Awake()
    {
        cropSelectionCanvas.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            FindFirstObjectByType<PlayerInventory>().AddtoInventory(2, 1);
            FindFirstObjectByType<PlayerInventory>().AddtoInventory(4, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject thingtoLook = Instantiate(gun, placetoInstantiate.transform.position, Quaternion.identity);
            thingtoLook.transform.parent = placetoInstantiate.transform;
            FindFirstObjectByType<LookAtCursor>().gun = thingtoLook;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject thingtoLook = Instantiate(utility, placetoInstantiate.transform.position, Quaternion.identity);
            thingtoLook.transform.parent = placetoInstantiate.transform;
            FindFirstObjectByType<LookAtCursor>().gun = thingtoLook;
        }
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
