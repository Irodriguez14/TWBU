using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharactersSaveFile 
{
    public string charName;
    public int lvl;
    public int currentHP;
    public int currentMajic;
    public bool guard;
    public int exp;
    public int turnsGuarded;
    public int pointsAbility;

    public CharactersSaveFile(string charName, int lvl, int currentHP, int currentMajic, bool guard, int exp, int turnsGuarded, int pointsAbility)
    {
        this.charName = charName;
        this.lvl = lvl;
        this.currentHP = currentHP;
        this.currentMajic = currentMajic;
        this.guard = guard;
        this.exp = exp;
        this.turnsGuarded = turnsGuarded;
        this.pointsAbility = pointsAbility;
    }
}
