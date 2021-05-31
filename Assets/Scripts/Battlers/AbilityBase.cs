using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Ability", menuName = "BattleCharacter/New Ability")]
public class AbilityBase : ScriptableObject
{
    [SerializeField] string abilityName;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] int power;
    [SerializeField] int majic;
    [SerializeField] int accuracy;
    [SerializeField] Sprite icon;
    [SerializeField] string abilityAnim;
    [SerializeField] bool basicAttack;
    [SerializeField] List<LearnableAbility> abilityToUnlock;
    [SerializeField] int pointsNeeded;
    public bool isPasive;

    public string getAbilityName() { return abilityName; }
    public string getDescription() { return description; }
    public int getPower() { return power; }
    public int getMajic() { return majic; }
    public int getAccuracy() { return accuracy; }
    public Sprite getIcon() { return icon; }
    public string getAbilityAnim() { return abilityAnim; }
    public bool isBasicAttack() { return basicAttack; }
    public List<LearnableAbility> getAbilityToUnlock() { return abilityToUnlock; }
    public int getPointsNeeded() { return pointsNeeded; }
}
