using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{

    Item item;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] ItemDetails itemPanel;
    [SerializeField] ItemUseMenu usePanel;
    [SerializeField] bool isInCombat;
    [SerializeField] DragDrop dragDrop;

    private void Awake()
    {
        //itemPanel = GameObject.FindGameObjectWithTag("ItemDetails").GetComponent<ItemDetails>(); 
    }

    public void addUiItem(Item newItem)
    {
        //Debug.Log("prueba");
        item = newItem;
        icon.sprite = item.item.getIcon();
        icon.enabled = true;
        if(newItem.amount > 1) {
            amountText.SetText(newItem.amount.ToString());
        } else {
            amountText.SetText(""); 
        }
        
    }

    public Item getItem() { return this.item; }

    public void clearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        amountText.SetText("");
    }

    public void clickItem()
    {
        if (!isInCombat) {
            itemPanel.gameObject.SetActive(true);
            itemPanel.showStats(item);
        } else {
            usePanel.gameObject.SetActive(true);
            usePanel.showStats(item);
        }
    }

    public void OnDrop(PointerEventData eventData) {
        //Debug.Log("onDrop");
        if(eventData.pointerDrag != null) {
            Item itemDropped = eventData.pointerDrag.transform.parent.gameObject.GetComponent<InventorySlot>().getItem();
            Debug.Log(itemDropped.item.getItemName());
            eventData.pointerDrag.gameObject.GetComponent<DragDrop>().changeItem(this.item);
            addUiItem(itemDropped);
            
        }
    }
}
