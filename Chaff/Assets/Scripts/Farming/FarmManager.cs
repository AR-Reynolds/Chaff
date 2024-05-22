using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public List<FarmObject> currentCrops;

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
            yield return new WaitForSeconds(1);
            Debug.Log("Attempting growth cycle");
            Grow();
        }
    }

}
