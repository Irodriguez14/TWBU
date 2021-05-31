using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTree : MonoBehaviour {

    [SerializeField] public BattleCharacterBase player;
    [SerializeField] List<GameObject> buttons;
    [SerializeField] TextMeshProUGUI pointsText;


    void Start() {
        loadAbilities();
    }

    public void unlockAbility() {
        
    }

    public void loadAbilities() {
        pointsText.text = player.pointsAbility.ToString();
        for (int i = 0; i < buttons.Count; i++) {
            if (i < player.getLearnableAbilities().Count) {
                if (!player.getLearnableAbilities()[i].getAbilityBase().isBasicAttack()) {
                    List<LearnableAbility> previousAbility = player.getLearnableAbilities()[i].getAbilityBase().getAbilityToUnlock();
                    buttons[i].GetComponent<SkillSlot>().setAbility(player.getLearnableAbilities()[i], player);
                    buttons[i].GetComponent<Image>().sprite = player.getLearnableAbilities()[i].getAbilityBase().getIcon();
                    int abilitiesNeeded = 0;
                    foreach(LearnableAbility lb in previousAbility) {
                        foreach(LearnableAbility lb2 in player.getLearnableAbilities()) {
                            if(lb != null) {
                                if (lb.getAbilityBase().getAbilityName() == lb2.getAbilityBase().getAbilityName()) {
                                    if (lb2.unlocked()) {
                                        abilitiesNeeded++;
                                    }
                                }
                            } else {
                                abilitiesNeeded++;
                            }
                            
                        }
                    }
                    if (abilitiesNeeded < previousAbility.Count && previousAbility.Count != 0) {
                        buttons[i].GetComponent<Image>().color = new Color32(92, 92, 92, 255);
                        buttons[i].GetComponent<Button>().interactable = false;
                    }
                    else {
                        buttons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                        buttons[i].GetComponent<Button>().interactable = true;
                    }
                }
            }
        }
    }

    bool checkIfCanBeUnlocked(AbilityBase baseSkill) {
        foreach (LearnableAbility ability in player.getLearnableAbilities()) {
            if (ability.getAbilityBase() == baseSkill) {
                return ability.unlocked();
            }
        }
        return true;
    }


    

}
