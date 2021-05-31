using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    MATERIAL,
    CONSUMABLE,
    WEAPON,
    ARMOR
}

public enum ItemRarity
{
    COMMON,
    RARE,
    VERY_RARE,
    ULTRA_RARE,
    EPIC,
    LEGENDARY,
    GOD
}
[System.Serializable]
public abstract class ItemBase : ScriptableObject
{
    [SerializeField] string itemName;
    [SerializeField] Sprite icon;
    public ItemType type;
    public int amount;
    [TextArea]
    [SerializeField] string description;
    [SerializeField] int priceToBuy;
    [SerializeField] int priceToSell;
    [SerializeField] bool stackable;


    public Sprite getIcon()
    {
        return this.icon;
    }

    public string getItemName()
    {
        return this.itemName;
    }
    public string getItemDescription()
    {
        return this.description;
    }

    public ItemType getItemType()
    {
        return this.type;
    }

    public bool isStackable()
    {
        return this.stackable;
    }

}
