using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 position;
    private GameObject parentGo;



    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        position = rectTransform.anchoredPosition;


    }

    public void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        parentGo = eventData.pointerDrag.gameObject.transform.parent.gameObject;
    }

    public void OnDrag(PointerEventData eventData) {

        Vector3 vector = eventData.delta / canvas.scaleFactor;
        rectTransform.localPosition += vector;

    }

    public void OnEndDrag(PointerEventData eventData) {
        //Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = position;
        //Debug.Log(parentGo);
    }

    public void OnPointerDown(PointerEventData eventData) {
        //Debug.Log("OnPointerDown");
    }

    public void changeItem(Item item) {
        if (item == null) {
            parentGo.gameObject.GetComponent<InventorySlot>().clearSlot();
        }
        else {
            parentGo.gameObject.GetComponent<InventorySlot>().addUiItem(item);
        }

    }

    public void itemBackToPosition() {
        rectTransform.anchoredPosition = position;
    }

    public void equipUI(Item newItem, Item actualItem) {
        if (actualItem.item == null) {
            Inventory.inventory.removeItem(newItem);
        }
        else {
            Inventory.inventory.addItem(actualItem.item);
            Inventory.inventory.removeItem(newItem);
        }
    }

}
