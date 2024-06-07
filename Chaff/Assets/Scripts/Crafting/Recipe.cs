using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe")]
public class Recipe : ScriptableObject
{
    public int craftingTime;

    public Item outputItem;
    public int outputQuantity;
    public int outputChance;

    [Serializable]
    public class InputItem
    {
        public Item inputItem;
        public int inputQuantity;
        public bool isConsumed;

    }

    public List<InputItem> inputItems;

}
