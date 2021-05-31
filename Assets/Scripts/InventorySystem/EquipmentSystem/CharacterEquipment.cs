using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterEquipment : MonoBehaviour
{
    public event EventHandler onEquipmentChanged;

    public delegate void OnItemNoEquipped();
    public OnItemNoEquipped onItemNoEquippedCallback;

    public delegate void OnItemEquipped(Item item, Item actualItem);
    public OnItemEquipped onItemEquippedCallback;

    public CharacterEquipment(Item headItem, Item trunkItem, Item legsItem, Item feetItem, Item weaponItem)
    {
        this.headItem = headItem;
        this.trunkItem = trunkItem;
        this.legsItem = legsItem;
        this.feetItem = feetItem;
        this.weaponItem = weaponItem;
    }

    public Item headItem;
    public Item trunkItem;
    public Item legsItem;
    public Item feetItem;
    public Item weaponItem;

    public Item getHeadItem() {
        return this.headItem;
    }
    public Item getTrunkItem() {
        return this.trunkItem;
    }
    public Item getLegsItem() {
        return this.legsItem;
    }
    public Item getFeetItem() {
        return this.feetItem;
    }
    public Item getWeaponItem()
    {
        return this.weaponItem;
    }
    public void Update()
    {
        Debug.Log(headItem.item);
    }

    public void setHeadItem(Item item) {
        this.headItem = item;
        onEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }
    public void setTrunkItem(Item item) {
        this.trunkItem = item;
        onEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }
    public void setLegsItem(Item item) {
        this.legsItem = item;
        onEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }
    public void setFeetItem(Item item) {
        this.feetItem = item;
        onEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }
    public void setWeaponItem(Item item)
    {
        this.weaponItem = item;
        onEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void tryEquipItem(ArmorType type, Item item, GameObject dragDrop) {
        if(item.item.getItemType() == ItemType.ARMOR) {
            ArmorBase armor = (ArmorBase)item.item;
            if(armor.getArmorType() == type) {
                switch (armor.getArmorType()) {
                    case ArmorType.HELMET: dragDrop.GetComponent<DragDrop>().equipUI(item, headItem); setHeadItem(item); break;
                    case ArmorType.CHESTPLATE: dragDrop.GetComponent<DragDrop>().equipUI(item, trunkItem); setTrunkItem(item); break;
                    case ArmorType.LEGGINGS: dragDrop.GetComponent<DragDrop>().equipUI(item, legsItem); setLegsItem(item);  break;
                    case ArmorType.BOOTS: dragDrop.GetComponent<DragDrop>().equipUI(item, feetItem); setFeetItem(item); break;
                }
            }
        }else if (item.item.getItemType() == ItemType.WEAPON) {
            dragDrop.GetComponent<DragDrop>().equipUI(item, weaponItem); setWeaponItem(item);
        }
        else {
            onItemNoEquippedCallback.Invoke();
        }


           

    }

}
