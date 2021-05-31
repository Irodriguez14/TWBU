using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType
{
    HEALTH,
    MAJIC,
    ATTACK,
    DEFENSE,
    SPEED
}


[CreateAssetMenu(fileName = "New consumable object", menuName = "Inventory/Items/Consumable")]
public class ConsumableBase : ItemBase
{
    [SerializeField] ConsumableType consumableType;
    [SerializeField] int amountConsumable;

    public void Awake()
    {
        type = ItemType.CONSUMABLE;
    }

    public int getAmountConsumable()
    {
        return this.amountConsumable;
    }

    public ConsumableType getConsumableType()
    {
        return this.consumableType;
    }
}
