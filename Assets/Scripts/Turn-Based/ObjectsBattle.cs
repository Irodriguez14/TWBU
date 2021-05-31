using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsBattle : MonoBehaviour
{
    Inventory inventory;
    public Transform itemsParent;
    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.inventory;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.items.Count) {
                if(inventory.items[i].item.type == ItemType.CONSUMABLE) {
                    slots[i].addUiItem(inventory.items[i]);
                } else {
                    slots[i].clearSlot();
                }
            } else {
                slots[i].clearSlot();
            }
        }
    }

    public void removeItemBattle(Item item, int amount)
    {
        inventory.takeAmountItem(item,amount);
    }
}
