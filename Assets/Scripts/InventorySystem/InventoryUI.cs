using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public Transform itemsParent;
    InventorySlot[] slots;
    [SerializeField] GameObject popupSpace;

    void Start()
    {
        inventory = Inventory.inventory;
        GameObject.FindGameObjectWithTag("ItemDetails").SetActive(false);
        inventory.onItemChangedCallback += UpdateUI;
        inventory.onSpaceFullCallback += popup;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        popupSpace.SetActive(false);
    }

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].addUiItem(inventory.items[i]);
            }
            else
            {
                slots[i].clearSlot();
            }
        }
    }

    private void popup()
    {
        StartCoroutine(spaceFull());
    }

    IEnumerator spaceFull()
    {
      popupSpace.SetActive(true);
      yield return new WaitForSeconds(1f);
      popupSpace.SetActive(false);
    }
}
