using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] private List<Recipe> recipes;

    [SerializeField] public Recipe selectedRecipe;
    bool isCrafting = false;
    Player player;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>();
    }

    public void SetRecipes()
    {
        int slotCount = 0;
        foreach (CraftingRecipeSlot recipe in player.recipeSlots)
        {
            if(slotCount >= recipes.Count)
            {
                return;
            }
            recipe.SetRecipe(recipes[slotCount]);
            slotCount++;
        }
    }

    public void CraftItem()
    {
        Debug.Log("fgdkksd");
        if (isCrafting) { return; }

        if(selectedRecipe != null)
        {
            Debug.Log("try craft");
            PlayerInventory inv = FindFirstObjectByType<PlayerInventory>();
            if(inv != null && inv.playerInventory.Count > 0)
            {
                bool crafting = true;

                for (int i = 0; i < selectedRecipe.inputItems.Count; i++)
                {
                    var inventoryItem = inv.FindInventoryItem(selectedRecipe.inputItems[i].inputItem.itemNumberID);
                    if (inventoryItem == null || inventoryItem.inventory_quantity < selectedRecipe.inputItems[i].inputQuantity)
                    {
                        Debug.Log("Cannot craft. Missing: " + selectedRecipe.inputItems[i].inputItem + " x" + selectedRecipe.inputItems[i].inputQuantity);
                        crafting = false;
                        return;
                    }
                    else
                    {
                        crafting = true;
                    }
                }
                if (crafting)
                {
                    for (int i = 0; i < selectedRecipe.inputItems.Count; i++)
                    {
                        inv.RemovefromInventory(selectedRecipe.inputItems[i].inputItem.itemNumberID, selectedRecipe.inputItems[i].inputQuantity);
                    }
                }    
                StopAllCoroutines();
                StartCoroutine(RecipeWaitTime(selectedRecipe.craftingTime));

            }
            else
            {
                if (inv != null)
                {
                    Debug.Log("Cannot craft. Missing: \n");
                    for (int x = 0; x < selectedRecipe.inputItems.Count; x++)
                    {
                        Debug.Log(selectedRecipe.inputItems[x].inputItem + " x" + selectedRecipe.inputItems[x].inputQuantity + "\n");
                    }
                    return;
                }
            }
        }
    }

    IEnumerator RecipeWaitTime(int seconds)
    {
        PlayerInventory inv = FindFirstObjectByType<PlayerInventory>();

        if (seconds > 0)
        {
            isCrafting = true;
            yield return new WaitForSeconds(seconds);
        }
        isCrafting = false;
        inv.AddtoInventory(selectedRecipe.outputItem.itemNumberID, selectedRecipe.outputQuantity);
        Debug.Log("Done craft!");
    }
}
