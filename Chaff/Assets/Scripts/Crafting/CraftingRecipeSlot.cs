using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipeSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeName;

    public Image slotImage;
    public Recipe slotRecipe;



    public void SetRecipe(Recipe recipe)
    {
        recipeName.text = recipe.outputItem.itemName + " x" + recipe.outputQuantity;
        slotRecipe = recipe;
    }
}
