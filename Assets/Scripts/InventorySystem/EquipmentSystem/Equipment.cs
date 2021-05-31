using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Equipment : MonoBehaviour
{
    [SerializeField] EquipmentSlotUI head;
    [SerializeField] EquipmentSlotUI trunk;
    [SerializeField] EquipmentSlotUI legs;
    [SerializeField] EquipmentSlotUI feet;
    [SerializeField] EquipmentSlotUI weapon;
    private CharacterEquipment characterEquipment;

    private void Awake() {
        head.OnItemDropped += OnItemDroppedHeadSlot;
        trunk.OnItemDropped += OnItemDroppedTrunkSlot;
        legs.OnItemDropped += OnItemDroppedLegsSlot;
        feet.OnItemDropped += OnItemDroppedFeetSlot;
        weapon.OnItemDropped += OnItemDroppedWeaponSlot;

        if(characterEquipment.getHeadItem().amount == 0) {
            head.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        else{
            head.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (characterEquipment.getTrunkItem().amount == 0) {
            trunk.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        } else {
            trunk.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (characterEquipment.getLegsItem().amount == 0) {
            legs.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        } else {
            legs.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (characterEquipment.getFeetItem().amount == 0) {
            feet.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        } else {
            feet.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        if (characterEquipment.getWeaponItem().amount == 0) {
            weapon.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        } else {
            weapon.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }


    private void OnItemDroppedHeadSlot(object sender, EquipmentSlotUI.OnItemDroppedEventArgs e) {
        characterEquipment.tryEquipItem(ArmorType.HELMET , e.item, e.eventData.pointerDrag.gameObject);
    }
    private void OnItemDroppedTrunkSlot(object sender, EquipmentSlotUI.OnItemDroppedEventArgs e) {
        characterEquipment.tryEquipItem(ArmorType.CHESTPLATE, e.item, e.eventData.pointerDrag.gameObject);
    }
    private void OnItemDroppedLegsSlot(object sender, EquipmentSlotUI.OnItemDroppedEventArgs e) {
        characterEquipment.tryEquipItem(ArmorType.LEGGINGS, e.item, e.eventData.pointerDrag.gameObject);
    }
    private void OnItemDroppedFeetSlot(object sender, EquipmentSlotUI.OnItemDroppedEventArgs e) {
        characterEquipment.tryEquipItem(ArmorType.BOOTS, e.item, e.eventData.pointerDrag.gameObject);
    }
    private void OnItemDroppedWeaponSlot(object sender, EquipmentSlotUI.OnItemDroppedEventArgs e){
        characterEquipment.tryEquipItem(ArmorType.ACCESORY, e.item, e.eventData.pointerDrag.gameObject);
    }

    public void setCharacterEquipment(CharacterEquipment characterEquipment) {
        this.characterEquipment = characterEquipment;
        updateVisual();

        characterEquipment.onEquipmentChanged += onEquipmentChanged;
    }

    private void onEquipmentChanged(object sender, System.EventArgs e) {
        updateVisual();
    }

    private void updateVisual() {
        if (characterEquipment.getHeadItem().amount != 0) {
            head.gameObject.GetComponent<Image>().sprite = characterEquipment.getHeadItem().item.getIcon();
            head.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        } else {

            head.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        if (characterEquipment.getTrunkItem().amount != 0) {
            trunk.gameObject.GetComponent<Image>().sprite = characterEquipment.getTrunkItem().item.getIcon();
            trunk.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        } else {
            trunk.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        if (characterEquipment.getLegsItem().amount != 0) {
            legs.gameObject.GetComponent<Image>().sprite = characterEquipment.getLegsItem().item.getIcon();
            legs.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        } else {
            legs.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        if (characterEquipment.getFeetItem().amount != 0) {
            feet.gameObject.GetComponent<Image>().sprite = characterEquipment.getFeetItem().item.getIcon();
            feet.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        } else {

            feet.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        if (characterEquipment.getWeaponItem().amount != 0) {
            weapon.gameObject.GetComponent<Image>().sprite = characterEquipment.getWeaponItem().item.getIcon();
            weapon.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else{
            weapon.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
    }
}
