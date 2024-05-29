using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] GameObject utility;
    [SerializeField] GameObject placetoInstantiate;

    [SerializeField] private Canvas cropSelectionCanvas;
    public GameObject cropPlot;

    private void Awake()
    {
        cropSelectionCanvas.enabled = false;
    }

    private void Update()
    {
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
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "PlantTrigger")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                FindFirstObjectByType<FarmManager>().Harvest(other.gameObject.transform.parent.gameObject);
            }
        }
        else if (other.gameObject.tag == "CropPlot")
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                cropSelectionCanvas.enabled = true;
                cropPlot = other.gameObject;
            }
        }
    }
}