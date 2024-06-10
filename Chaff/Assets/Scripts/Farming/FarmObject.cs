using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using UnityEngine;

public class FarmObject : MonoBehaviour
{
    [Header("Output Items")]
    public List<OutputItem> outputs;

    [Header("Growth Stages")]
    [SerializeField] List<GameObject> plantStages;

    [Header("Plant Settings")]
    public int maxGrowthLevel = 5;
    public float growthChance = 35f;
    public float growthRandomness = 0.5f;
    public bool harvestable = false;
    public bool reusable = false;
    public bool resizable = false;
    public float resizeScale = 0.5f;

    [Header("References")]
    public int currentGrowthLevel = 0;
    public int cropID = 0;
    public string cropNameID = "Corn";
    [SerializeField] public Item plantSeed;
    [SerializeField] GameObject harvestTrigger;
    // optional
    [SerializeField] GameObject growingCrop;
    [SerializeField] GameObject plantStage;

    private Vector3 initSize;

    private void Awake()
    {
        initSize = transform.localScale;
        growthChance += (Random.Range(-15 * growthRandomness, 15 * growthRandomness));
        if(growthChance < 5)
        {
            growthChance = 5;
        }
    }

    public void GrowthScale()
    {
        if(plantStage.transform.childCount == 0)
        {
            GameObject currentStage = Instantiate(plantStages[currentGrowthLevel]);
            currentStage.transform.parent = plantStage.transform;
            currentStage.transform.position = plantStage.transform.position;
        }
        else
        {
            for(int i = 0; i < plantStage.transform.childCount; i++)
            {
                Destroy(plantStage.transform.GetChild(i).gameObject);
            }
            GameObject currentStage = Instantiate(plantStages[currentGrowthLevel - 1]);
            currentStage.transform.parent = plantStage.transform;
            currentStage.transform.position = plantStage.transform.position;
        }

        if(resizable)
        {
            Vector3 currentGrowth = new Vector3(currentGrowthLevel, currentGrowthLevel, currentGrowthLevel);

            growingCrop.transform.DOScale(growingCrop.transform.localScale + currentGrowth * resizeScale, 1.5f);
        }
    }

    public void ResetPlant()
    {
        currentGrowthLevel = 0;
        growingCrop.transform.localScale = initSize;

        for (int i = 0; i < plantStage.transform.childCount; i++)
        {
            Destroy(plantStage.transform.GetChild(i).gameObject);
        }
        GameObject currentStage = Instantiate(plantStages[currentGrowthLevel]);
        currentStage.transform.parent = plantStage.transform;
        currentStage.transform.position = plantStage.transform.position;
    }
}
