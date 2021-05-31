using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    SWORD,
    SPEAR,
    BOW,
    HEAVY,
    GLOVES
}

[CreateAssetMenu(fileName = "New weapon object", menuName = "Inventory/Items/Weapon")]
public class WeaponBase : ItemBase
{
    [SerializeField] int lvl;
    [SerializeField] int attack;
    [SerializeField] double crit;
    [SerializeField] ItemRarity rarity;
    [SerializeField] WeaponType weaponType;
    [SerializeField] List<MaterialBase> materialsToCraft;

    public void Awake()
    {
        type = ItemType.WEAPON;
    }
    public int getLvl()
    {
        return this.lvl;
    }
    public ItemRarity getRarity()
    {
        return this.rarity;
    }
}
