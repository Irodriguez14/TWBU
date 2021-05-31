using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlot : MonoBehaviour
{
    private LearnableAbility ability;
    private BattleCharacterBase player;
    [SerializeField] GameObject details;

    public void setAbility(LearnableAbility abilityBase, BattleCharacterBase player) {
        this.ability = abilityBase;
        this.player = player;
    }

    public void clickItem() {
        details.gameObject.SetActive(true);
        details.GetComponent<SkillDetails>().showStats(ability, player, this.gameObject);

    }
}
