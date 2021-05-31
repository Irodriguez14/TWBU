using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BattleUnit : MonoBehaviour
{
    [SerializeField] BattleCharacterBase bcb;
    [SerializeField] int lvl;
    [SerializeField] bool playerUnit;
    [SerializeField] Button button;
    [SerializeField] Image turnSelector;
    public bool dead;

    public BattleCharacter bc;
    public BattleUnit() { }

    public void setup()
    {
        if(bc == null) bc = new BattleCharacter(this.bcb, this.lvl);
        if (button != null) {
            makeButtonInteractable(false);
        }
        GetComponent<Image>().sprite = bc.getBattleCharacterBase().getSprite();
        dead = false;
    }
    public void setupWithData(BattleCharacter bc)
    {
        this.bc = bc;
        if (button != null) {
            makeButtonInteractable(false);
        }
        GetComponent<Image>().sprite = bc.getBattleCharacterBase().getSprite();
        this.bc.abilities = new List<Ability>();
        foreach (var ability in this.bc.getBattleCharacterBase().getLearnableAbilities()) {
            if (ability.unlocked()) {
                if (!ability.getAbilityBase().isPasive) {
                    this.bc.abilities.Add(new Ability(ability.getAbilityBase()));
                }
            }
        }
    }


    public void enableTurn() {
        turnSelector.gameObject.SetActive(true);
    }
    public void disableTurn() {
        turnSelector.gameObject.SetActive(false);
    }

    public BattleCharacter getBattleCharacter(){ return bc; }

    public void makeButtonInteractable(bool interactable)
    {
        button.interactable = interactable;
    }

    public void setBcb(BattleCharacterBase bcb)
    {
        this.bcb = bcb;
    }
    public void setLvl(int lvl)
    {
        this.lvl = lvl;
    }
    public void setPlayerUnit()
    {
        this.playerUnit = true;
    }
    public void setPlayerUnit(bool value)
    {
        this.playerUnit = value;
    }
    public void setButton(Button button)
    {
        this.button = button;
    }

    public void setTurnImage(Image image) {
        this.turnSelector = image;
    }
    public int getLvl()
    {
        return this.lvl;
    }
    public BattleCharacterBase getBcb()
    {
        return this.bcb;
    }

    public bool isPlayerUnit()
    {
        return playerUnit;
    }
    public bool isDead()
    {
        return dead;
    }
    public void setDead()
    {
        this.dead = true;
    }

    public static int SortBySpeed(BattleUnit p1, BattleUnit p2)
    {
        if(p1.getBcb().getSpeed() == p2.getBcb().getSpeed())
        {
            int random = Random.Range(1, 3);
            if(random == 1)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return p1.getBcb().getSpeed().CompareTo(p2.getBcb().getSpeed());
        }
       
    }
}
