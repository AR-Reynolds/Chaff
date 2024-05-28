using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public List<FarmObject> currentCrops;
    public int growthCheckInSeconds = 1;

    private bool growth;

    public void Start()
    {
        StartCoroutine(GrowthCheck());
    }

    private void Grow()
    {
        foreach (FarmObject item in currentCrops)
        {
            int growthCheck = 0;
            if (item.currentGrowthLevel != item.maxGrowthLevel)
            {
                growthCheck = Random.Range(0, 100);
            }

            if (item.currentGrowthLevel != item.maxGrowthLevel && growthCheck <= item.growthChance)
            {
                item.currentGrowthLevel++;
                item.GrowthScale();
            }
            if (item.currentGrowthLevel == item.maxGrowthLevel)
            {
                item.harvestable = true;
            }
        }
    }

    private IEnumerator GrowthCheck()
    {
        growth = true;
        while (growth)
        {
            yield return new WaitForSeconds(growthCheckInSeconds);
            Debug.Log("Attempting growth cycle");
            Grow();
        }
    }

    public void Harvest(GameObject plantToHarvest)
    {
        FarmObject farmObj = plantToHarvest.GetComponent<FarmObject>();
        if(farmObj == null || !farmObj.harvestable)
        {
            return;
        }
        if(farmObj.reusable)
        {
            farmObj.harvestable = false;
            farmObj.ResetPlant();
            foreach(var output in farmObj.outputs)
            {
                int randomChance = Random.Range(0, 100);
                if(randomChance <= output.outputChance)
                {
                    FindFirstObjectByType<PlayerInventory>().AddtoInventory(output.item.itemNumberID, output.outputAmount);
                }
            }
        }
        else
        {
            farmObj.harvestable = false;
            foreach (var output in farmObj.outputs)
            {
                int randomChance = Random.Range(0, 100);
                if (randomChance <= output.outputChance)
                {
                    FindFirstObjectByType<PlayerInventory>().AddtoInventory(output.item.itemNumberID, output.outputAmount);
                }
            }
            Destroy(plantToHarvest);
        }
    }

}
