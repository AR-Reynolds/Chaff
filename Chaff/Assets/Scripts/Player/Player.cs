using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        
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
    }
}
