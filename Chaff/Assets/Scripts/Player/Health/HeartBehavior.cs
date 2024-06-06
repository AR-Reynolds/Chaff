using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBehavior : MonoBehaviour
{
    public float heartSize = 1;
    public int heartValue = 20;

    RectTransform heart;

    private void Awake()
    {
        heart = GetComponent<RectTransform>();
        heartSize = 1;
    }

    public void UpdateHeartUI(int health)
    {
        if(health > 0)
        {
            heartSize = (health * 0.01f) * 5;
        }
        else
        {
            heartSize = 0;
        }


        if (heartSize <= 0)
        {
            heartSize = 0;
            heart.localScale = Vector3.zero;
            return;
        }
        if (heartSize >= 1)
        {
            heartSize = 1;
            heart.localScale = Vector3.one;
            return;
        }
        heart.localScale = new Vector3(heartSize, heartSize, heartSize);
    }
}
