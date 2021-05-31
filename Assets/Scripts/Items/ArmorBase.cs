using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType
{
    HELMET,
    CHESTPLATE,
    LEGGINGS,
    BOOTS,
    ACCESORY
}

[CreateAssetMenu(fileName = "New armor object", menuName = "Inventory/Items/Armor")]
public class ArmorBase : ItemBase
{
    [SerializeField] int lvl;
    [SerializeField] int defense;
    [SerializeField] int health;
    [SerializeField] ItemRarity rarity;
    [SerializeField] ArmorType armorType;
    [SerializeField] List<MaterialBase> materialsToCraft;


    public void Awake()
    {
        type = ItemType.ARMOR;
    }

    public int getLvl()
    {
        return this.lvl;
    }
    public ItemRarity getRarity()
    {
        return this.rarity;
    }

    public ArmorType getArmorType() { return this.armorType; }


}
