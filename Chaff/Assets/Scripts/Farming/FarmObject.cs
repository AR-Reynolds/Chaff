using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmObject : MonoBehaviour
{
    public int currentGrowthLevel = 0;
    public bool harvestable = false;
    public int outputAmount = 1;

    public int maxGrowthLevel = 5;
    public float growthChance = 35f;

    public void GrowthScale()
    {
        float x = gameObject.transform.localScale.x;
        float y = gameObject.transform.localScale.y;
        float z = gameObject.transform.localScale.z;

        gameObject.transform.localScale = new Vector3(x, y + currentGrowthLevel * 0.1f, z);
    }
}
