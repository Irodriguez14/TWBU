using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemsSaveFile
{
    public int amount;
    public string itemName;

    public ItemsSaveFile(int amount, string itemName)
    {
        this.amount = amount;
        this.itemName = itemName;
    }


}
