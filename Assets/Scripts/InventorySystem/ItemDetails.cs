using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour
{
    Item item;
    [SerializeField] Image iconDetails;
    [SerializeField] TextMeshProUGUI nameDetails;
    [SerializeField] TextMeshProUGUI descriptionDetails;
    [SerializeField] TextMeshProUGUI lvlDetails;
    [SerializeField] TextMeshProUGUI rarityDetails;
    [SerializeField] TextMeshProUGUI statsDetails;
    [SerializeField] GameObject closePanel;

    [SerializeField] TextMeshProUGUI amountDelete;
    [SerializeField] Button plusButton;
    [SerializeField] Button minusButton;
    [SerializeField] GameObject deletePanel;


    public void showStats(Item item){
        this.item = item;
        closePanel.SetActive(true);
        iconDetails.sprite = item.item.getIcon();
        nameDetails.SetText(item.item.getItemName());
        descriptionDetails.SetText(item.item.getItemDescription());
        if(item.item.getItemType() == ItemType.ARMOR) {
            ArmorBase ab = (ArmorBase)item.item;
            lvlDetails.SetText("LVL: " + ab.getLvl().ToString());
            rarityDetails.SetText(ab.getRarity().ToString());
        } else if(item.item.getItemType() == ItemType.WEAPON) {
            WeaponBase wb = (WeaponBase)item.item;
            lvlDetails.SetText("LVL: " + wb.getLvl().ToString());
            rarityDetails.SetText(wb.getRarity().ToString());
        }
        amountDelete.SetText("1");
        //statsDetails.SetText(item.item.name);
    }
    private void Start() {
        deletePanel.SetActive(false);

    }
    private void Update()
    {

        if(item.amount < int.Parse(amountDelete.text) + 1) {
            plusButton.interactable = false;
        } else {
            plusButton.interactable = true;
        }
        if (int.Parse(amountDelete.text) -1 > 0) {
            minusButton.interactable = true;
        } else {
            minusButton.interactable = false;
        }
    }
    public void closeDetails()
    {
        closePanel.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void addAmountToDelete()
    {
        int newAmount = int.Parse(amountDelete.text) + 1;
        amountDelete.SetText(newAmount.ToString());
    }

    public void minusAmountToDelete()
    {
        int newAmount = int.Parse(amountDelete.text) + -1;
        amountDelete.SetText(newAmount.ToString());
    }

    public void removeItem()
    {
        int amountToRemove = int.Parse(amountDelete.text);
        if(item.amount == amountToRemove)
        {
            Inventory.inventory.removeItem(item);
        }
        else
        {
            Inventory.inventory.takeAmountItem(item, amountToRemove);
        }
        Debug.Log("dentro");
        StartCoroutine(popUp());
    }

    IEnumerator popUp() {
        Debug.Log("CoroutineExample started at " + Time.time.ToString() + "s");
        deletePanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Coroutine Iteration Successful at " + Time.time.ToString() + "s");
        deletePanel.SetActive(false);
        closeDetails();
        Debug.Log("fin");
    }

}
