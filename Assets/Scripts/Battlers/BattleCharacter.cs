using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleCharacter {
    BattleCharacterBase battleCharacterBase;
    int lvl;
    int currentHP;
    int currentMajic;
    bool guard;
    int exp;
    private int turnsGuarded;

    public List<Ability> abilities { get; set; }
    public BattleCharacter(BattleCharacterBase bcb, int lvl) {
        battleCharacterBase = bcb;
        battleCharacterBase.actualAttack = battleCharacterBase.getAttack();
        battleCharacterBase.actualDefense = battleCharacterBase.getDefense();
        battleCharacterBase.actualMajic = battleCharacterBase.getMajic();
        battleCharacterBase.actualSpeed = battleCharacterBase.getSpeed();
        this.lvl = lvl;
        battleCharacterBase.actualMaxHp = bcb.getMaxHp();
        this.currentMajic = bcb.getMajic();
        this.guard = false;
        recalculateStats();
        this.currentHP = battleCharacterBase.actualMaxHp;
        abilities = new List<Ability>();
        foreach (var ability in battleCharacterBase.getLearnableAbilities()) {
            if (ability.unlocked()) {
                if (!ability.getAbilityBase().isPasive) {
                    abilities.Add(new Ability(ability.getAbilityBase()));
                }
            }
        }
        battleCharacterBase.pointsAbility = 1;
    }
    public void heal() {
        this.currentHP = battleCharacterBase.actualMaxHp;
        this.currentMajic = battleCharacterBase.actualMajic;
    }

    public BattleCharacter(BattleCharacterBase bcb, CharactersSaveFile saved)
    {
        battleCharacterBase = bcb;
        this.currentHP = saved.currentHP;
        this.currentMajic = saved.currentMajic;
        this.lvl = saved.lvl;
        this.guard = saved.guard;
        this.turnsGuarded = saved.turnsGuarded;
        abilities = new List<Ability>();
        foreach (var ability in battleCharacterBase.getLearnableAbilities()) {
            if (ability.unlocked()) {
                abilities.Add(new Ability(ability.getAbilityBase()));
            }
        }
        this.exp = saved.exp;
        battleCharacterBase.pointsAbility = saved.pointsAbility;
    }

    public BattleCharacterBase getBattleCharacterBase() { return battleCharacterBase; }
    public int getLvl() { return lvl; }
    public int getCurrentMajic() { return currentMajic; }
    public int getExp() { return exp; }
    public int getTurnsGuarded() { return turnsGuarded; }
    public int getCurrentHP() { return currentHP; }
    public bool isGuard() { return guard; }
    public void addHp(int hpToAdd) {
        this.currentHP += hpToAdd;
        if(this.currentHP > battleCharacterBase.actualMaxHp) {
            this.currentHP = battleCharacterBase.actualMaxHp;
        }


    }

    public void toggleGuard() {
        this.guard = !this.guard;
    }

    public bool TakeDamage(Ability ability, BattleCharacter attacker) {
        //TODO make final formula
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.getLvl() + 10) / 250f;
        float d = a * ability.abilityBase.getPower() * ((float)attacker.battleCharacterBase.actualAttack / battleCharacterBase.actualAttack) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        currentHP -= damage;
        if (currentHP <= 0) {
            currentHP = 0;
            return true;
        }
        return false;
    }

    void recalculateStats() {
        battleCharacterBase.actualMaxHp = (int)(this.lvl * 1.5f) + battleCharacterBase.actualMaxHp;
        if(this.lvl == 1) {
            battleCharacterBase.actualExpToLvlUp = 20;
        } else if(this.lvl == 2) {
            battleCharacterBase.actualExpToLvlUp = 30;
        }
        else{
            battleCharacterBase.actualExpToLvlUp = (int)(1.2f * (Mathf.Pow(lvl, 3)) - 15 * (Mathf.Pow(lvl, 2)) + (100 * lvl) - 140);
        }
        Debug.Log(this.lvl);

        // dar a elegir puntos para subir, 1 por 


        //Hp : actualHP + (lvl * 1,5)
        //lvl1: 51,5  si hp = 50
        //lvl2: 53
        //lvl3: 59     32,4 - 135 + 300 - 140
        //lvl4: 65

        //1,2 * (Math.Pow(lvl,3)) - 15 * (Math.Pow(lvl,2) + 100 * lvl - 140 -> ESTO A PARTIR DE LVL3 ------- ANTES LVL1 -> 20EXP_TO_LVL_UP --- LVL2 -> 35EXP

    }

    public bool addExperience(int expToAdd) {
        this.exp += expToAdd;
        if(this.exp >= battleCharacterBase.actualExpToLvlUp) {
            this.lvl++;
            this.battleCharacterBase.pointsAbility++;
            Debug.Log("Has subido de nivel");
            this.exp = 0;
            int bufferExp = expToAdd - battleCharacterBase.actualExpToLvlUp;
            this.exp += bufferExp;
            Debug.Log(this.exp + " expActual ");
            recalculateStats();
            return true;
        }
        return false;
    }


    public bool calculateMajic(Ability ability, BattleCharacter attacker) {
        if (currentMajic - ability.getAbilityBase().getMajic() < 0) {
            return false;
        }
        else {
            currentMajic -= ability.getAbilityBase().getMajic();
            return true;
        }
    }

    public Ability getRandomAbility() {
        bool hasMajicForAbility = false;
        int r = -1;
        for(int i = 0; !hasMajicForAbility; i++) {
            r = Random.Range(0, abilities.Count);
            if (currentMajic - abilities[r].getAbilityBase().getMajic() < 0) {
                hasMajicForAbility = false;
            }
            else {
                currentMajic -= abilities[r].getAbilityBase().getMajic();
                hasMajicForAbility = true;
            }
        }

        return abilities[r];
    }

    public void calculateStats() {
        calculateAttack();
        calculateDefense();
        calculateSpeed();
        calculateMajic();
        calculateCrit();
        calculateMaxHP();
    }
    private int calculateAttack() {
        return Mathf.FloorToInt((battleCharacterBase.getAttack() * this.lvl) / 100f) + 5;
    }
    private int calculateDefense() {
        return Mathf.FloorToInt((battleCharacterBase.getDefense() * this.lvl) / 100f) + 5;
    }
    private void calculateSpeed() {
        battleCharacterBase.setSpeed(Mathf.FloorToInt((battleCharacterBase.getAttack() * this.lvl) / 100f) + 5);
    }
    private void calculateMajic() {
        battleCharacterBase.setMajic(battleCharacterBase.getAttack() + (this.lvl * battleCharacterBase.getAttack()) + 10);
    }
    private void calculateCrit() {
        battleCharacterBase.setCrit(battleCharacterBase.getAttack() + (this.lvl * battleCharacterBase.getAttack()));
    }
    private void calculateMaxHP() {
        battleCharacterBase.setMaxHp(battleCharacterBase.getAttack() + (this.lvl * battleCharacterBase.getAttack()) + 100);
    }



}
