using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class EquipmentSlotUI : MonoBehaviour, IDropHandler {

    public event EventHandler<OnItemDroppedEventArgs> OnItemDropped;

    public class OnItemDroppedEventArgs : EventArgs {
        public Item item;
        public PointerEventData eventData;
    }
    public void OnDrop(PointerEventData eventData) {
        Item item = eventData.pointerDrag.gameObject.transform.parent.gameObject.GetComponent<InventorySlot>().getItem();
        OnItemDropped?.Invoke(this, new OnItemDroppedEventArgs { item = item, eventData = eventData});
    }
}
