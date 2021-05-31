using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LearnableAbility {
    [SerializeField] AbilityBase abilityBase;
    [SerializeField] bool isUnlocked;

    public AbilityBase getAbilityBase() { return abilityBase; }
    public bool unlocked() { return isUnlocked; }

    public void unlock() { isUnlocked = true; }

}
