using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BattleCharacter", menuName = "BattleCharacter/New BattleCharacter")]
public class BattleCharacterBase : ScriptableObject
{

    [SerializeField] string characterName;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite sprite;

    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int speed;
    [SerializeField] int majic;
    [SerializeField] int crit;
    [SerializeField] int expToLvlUp;
    [SerializeField] string idleAnim;

    public Sprite icon;
    public string idleAnimZoom;
    public int pointsAbility;

    public int actualMaxHp;
    public int actualAttack;
    public int actualDefense;
    public int actualSpeed;
    public int actualMajic;
    public int actualExpToLvlUp;
    


    [SerializeField] List<LearnableAbility> learnableAbilities;



    public string getCharacterName(){ return characterName;}
    public string getDescription(){ return description;}
    public Sprite getSprite(){ return sprite;}
    public int getMaxHp(){ return maxHp;}
    public int getAttack(){ return attack;}
    public int getDefense(){ return defense;}
    public int getSpeed(){ return speed;}
    public int getMajic(){ return majic;}
    public int getCrit(){ return crit; }
    public int getExpToLvlUp(){ return expToLvlUp; }
    public string getIdleAnim() { return idleAnim; }
    public List<LearnableAbility> getLearnableAbilities() { return learnableAbilities; }

    public void setMaxHp(int maxHP) {  this.maxHp = maxHP; }
    public void setAttack(int attack) { this.attack = attack; }
    public void setDefense(int defense) { this.defense = defense; }
    public void setSpeed(int speed) { this.speed = speed; }
    public void setMajic(int majic) { this.majic = majic; }
    public void setCrit(int crit) { this.crit = crit; }

    public int getPoints() { return pointsAbility; }
    public void addPoints() { pointsAbility++; }




}
