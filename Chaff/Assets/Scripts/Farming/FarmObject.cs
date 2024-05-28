using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmObject : MonoBehaviour
{
    [SerializeField] GameObject harvestTrigger;
    public int currentGrowthLevel = 0;
    public bool harvestable = false;
    public bool reusable = false;

    public int maxGrowthLevel = 5;
    public float growthChance = 35f;
    public List<OutputItem> outputs;

    private Vector3 initSize;

    private void Awake()
    {
        initSize = transform.localScale;
    }

    public void GrowthScale()
    {
        float x = gameObject.transform.localScale.x;
        float y = gameObject.transform.localScale.y;
        float z = gameObject.transform.localScale.z;

        gameObject.transform.localScale = new Vector3(x, y + currentGrowthLevel * 0.1f, z);
    }

    public void ResetPlant()
    {
        {
            currentGrowthLevel = 0;
            gameObject.transform.localScale = initSize;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
