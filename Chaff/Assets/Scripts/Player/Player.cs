using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject placetoInstantiate;

    public Canvas cropSelectionCanvas;
    public Canvas recipeSelectionCanvas;
    public List<CraftingRecipeSlot> recipeSlots;
    public GameObject cropPlot;

    [SerializeField] private CraftingManager manager;

    private void Awake()
    {
        cropSelectionCanvas.enabled = false;
        recipeSelectionCanvas.enabled = true; 
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "PlantTrigger":
                if (Input.GetKeyDown(KeyCode.F))
                {
                    FindFirstObjectByType<FarmManager>().Harvest(other.gameObject.transform.parent.gameObject);
                }
                break;
            case "CraftingTrigger":
                if (manager == null)
                {
                    manager = other.transform.parent.GetComponent<CraftingManager>();
                }
                break;
            case "CropPlot":
                if (!other.gameObject.GetComponent<CropPlot>().plotUsed)
                {
                    cropSelectionCanvas.enabled = true;
                    cropPlot = other.gameObject;
                }
                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CraftingTrigger")
        {
            recipeSelectionCanvas.enabled = true;
            manager = other.transform.parent.GetComponent<CraftingManager>();
            manager.SetRecipes();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CropPlot")
        {
            cropSelectionCanvas.enabled = false;
            cropPlot = null;
        }
        else if (other.gameObject.tag == "CraftingTrigger")
        {
            recipeSelectionCanvas.enabled = false;
            manager = null;
        }
    }

    public void Craft()
    {
        if(manager != null)
        {
            manager.CraftItem();
        }
    }
    public void SelectRecipe(Button button)
    {
        if (manager != null && button.GetComponent<CraftingRecipeSlot>().slotRecipe != null)
        {
            manager.selectedRecipe = button.GetComponent<CraftingRecipeSlot>().slotRecipe;
        }
    }
}
