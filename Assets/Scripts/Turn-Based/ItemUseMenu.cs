using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseMenu : MonoBehaviour
{
    Item item;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI nameDetails;
    [SerializeField] TextMeshProUGUI descriptionDetails;
    [SerializeField] Button useButton;

    public void showStats(Item item)
    {
        this.item = item;
        icon.sprite = item.item.getIcon();
        nameDetails.SetText(item.item.getItemName());
        descriptionDetails.SetText(item.item.getItemDescription());
        
    }

    public Item getItem()
    {
        return this.item;
    }
}
