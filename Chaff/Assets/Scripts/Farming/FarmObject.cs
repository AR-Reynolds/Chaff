using System.Collections;
using System.Collections.Generic;
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
    public bool harvestable = false;
    public bool reusable = false;

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
        growthChance += (Random.Range(-10, 10));
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
            GameObject currentStage = Instantiate(plantStages[currentGrowthLevel]);
            currentStage.transform.parent = plantStage.transform;
            currentStage.transform.position = plantStage.transform.position;
        }

        if(reusable)
        {
            float x = growingCrop.transform.localScale.x;
            float y = growingCrop.transform.localScale.y;
            float z = growingCrop.transform.localScale.z;

            growingCrop.transform.localScale = new Vector3(x + currentGrowthLevel * 0.2f, y + currentGrowthLevel * 0.2f, z + currentGrowthLevel * 0.2f);
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
