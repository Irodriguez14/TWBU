using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public ItemBase item;
    public int amount;

    public Item(ItemBase item)
    {
        this.item = item;
        this.amount = 1;
    }

    public Item(ItemBase item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }


}
