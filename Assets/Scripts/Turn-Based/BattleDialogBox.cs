using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int letterPerSecond;

    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;

    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<TextMeshProUGUI> moveTexts;
    [SerializeField] List<Image> moveImages;
    [SerializeField] List<Button> moveButton;

    [SerializeField] Text majicText;


    public void setDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator typeDialog(string dialog)
    {
        dialogText.text = "";
        foreach(var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
    }

    public void enableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }
    public void enableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }
    public void enableMoveSelector(bool enabled)
    {
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }

    public void setMoveNames(List<Ability> abilities) 
    {
        for(int i = 0; i < moveTexts.Count; i++)
        {
            if(i < abilities.Count)
            {
                if (!abilities[i].abilityBase.isBasicAttack()) {
                    moveTexts[i].text = abilities[i].getAbilityBase().getAbilityName();
                    moveImages[i].sprite = abilities[i].getAbilityBase().getIcon();
                    moveButton[i].enabled = true;
                }
                else {
                    moveImages[i].transform.gameObject.SetActive(false);
                    moveTexts[i].text = "-----------------------";
                    moveButton[i].enabled = false;
                }
            }
            else{
                moveImages[i].transform.gameObject.SetActive(false);
                moveTexts[i].text = "-----------------------";
                moveButton[i].enabled = false;
            }
        }
    }

    public void buttonsActionSelector(bool interactable)
    {
        foreach(Transform children in actionSelector.transform) {
            children.GetComponent<Button>().interactable = interactable;
        }
    }
   

}
