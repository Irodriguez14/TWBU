using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillDetails : MonoBehaviour
{
    private LearnableAbility ability;
    private BattleCharacterBase player;
    [SerializeField] Image iconDetails;
    [SerializeField] TextMeshProUGUI nameDetails;
    [SerializeField] TextMeshProUGUI descriptionDetails;
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] GameObject closePanel;
    [SerializeField] AbilityTree abilitySystem;
    [SerializeField] GameObject popupSpace;
    [SerializeField] GameObject popupUnlock;
    [SerializeField] Button buttonConfirm;
    private GameObject skillSlot;

    public void showStats(LearnableAbility ability, BattleCharacterBase player, GameObject skillSlot)
    {
        this.ability = ability;
        this.player = player;
        this.iconDetails.sprite = this.ability.getAbilityBase().getIcon();
        this.nameDetails.text = this.ability.getAbilityBase().getAbilityName();
        this.descriptionDetails.text = this.ability.getAbilityBase().getDescription();
        this.points.text = this.ability.getAbilityBase().getPointsNeeded().ToString();
        this.closePanel.gameObject.SetActive(true);
        this.skillSlot = skillSlot;
        if (this.ability.unlocked()) {
            buttonConfirm.interactable = false;
            buttonConfirm.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Desbloqueada";
            buttonConfirm.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Desbloqueada";
        } else {
            buttonConfirm.interactable = true;
            buttonConfirm.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Desbloquear";
            buttonConfirm.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Desbloquear";
        }
    }

    public void closeDetails()
    {
        closePanel.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void unlockAbility()
    {
        if (!ability.unlocked()) {
            buttonConfirm.interactable = true;
            buttonConfirm.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Desbloquear";
            if (ability.getAbilityBase().getPointsNeeded() <= player.pointsAbility) {
                foreach (LearnableAbility ab in player.getLearnableAbilities()) {
                    if (ab.Equals(ability)) {
                        ab.unlock();
                        StartCoroutine(popUpUnlock());
                        buttonConfirm.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Desbloqueada";
                        skillSlot.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                        this.closePanel.gameObject.SetActive(false);
                        player.pointsAbility -= ability.getAbilityBase().getPointsNeeded();
                        abilitySystem.loadAbilities();
                    }
                }
            } else {

                StartCoroutine(spaceFull());
            }
        }

    }

    IEnumerator spaceFull()
    {
        popupSpace.SetActive(true);
        yield return new WaitForSeconds(1f);
        popupSpace.SetActive(false);
    }

    IEnumerator popUpUnlock()
    {
        popupUnlock.SetActive(true);
        yield return new WaitForSeconds(1f);
        popupUnlock.SetActive(false);
        gameObject.SetActive(false);
    }


}
