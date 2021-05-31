using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability
{
    public AbilityBase abilityBase;
    

    public Ability(AbilityBase ab)
    {
        abilityBase = ab;
    }

    public AbilityBase getAbilityBase()
    {
        return this.abilityBase;
    }
    
}
