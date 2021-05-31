using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public delegate void OnSpaceFull();
    public OnSpaceFull onSpaceFullCallback;

    public List<Item> items = new List<Item>();

    [SerializeField] int space = 20;
    public ItemBase prueba;

    private bool isInicializated = false;

    #region Singleton
    public static Inventory inventory;
    private void Awake()
    {
        if(inventory != null)
        {
            Debug.Log("Mas de una instancia del inventario encontrada");
            return;
        }

        inventory = this;
        
    }
    #endregion
      
    private void Update() {
        foreach(Item i in items) {
            Debug.Log(i.item + " amount: " + i.amount);
        }
        if (onItemChangedCallback != null && !isInicializated) {
            onItemChangedCallback.Invoke();
            isInicializated = true;
        }

    }

    public void addItem(ItemBase item)
    {
        if(items.Count >= space)
        {
            Debug.Log("No hay espacio");
            if (onSpaceFullCallback != null) {
                onSpaceFullCallback.Invoke();
            }
            return;
        }
        if (item.isStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in items)
            {
                if(inventoryItem.item.name == item.name)
                {
                    if (inventoryItem.amount < 20) {
                        inventoryItem.amount++;
                        itemAlreadyInInventory = true;
                    }
                }
            }
            if (!itemAlreadyInInventory) {
                Item newItem = new Item(item);
                items.Add(newItem);
            }
        } else {
            Item newItem = new Item(item);
            items.Add(newItem);
        }
       
        if(onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public void removeItem(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public void takeAmountItem(Item item, int amountToTake){
        foreach(Item i in items){
            if(i == item) {
                i.amount -= amountToTake;
            }
        }
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }



}
