using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public MoveTo moveTo;
    public DialogueBase dialogue;
    public DialogueBase dialogueAlt;
    public ItemBase itemGift;

    public bool isMonarca;
    public bool isItemFound;
    public bool isCroquetillaTown1;
    public bool isBayaRojaTown1;
    public bool isCuerdaRuta1;
    public bool isCremaIkigai;
    public bool isOroRuta2;
    public bool isOnlyDialog;
    public bool isHealer;
    public bool isArmaGod;
    public bool isEndTown1;
    public bool isAuto;
    public bool isFishMan;


    void Start()
    {

    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            if (isAuto) {
                DialogueManager.instance.EnqueueDialogue(dialogue);
                GameManage.instance.player.gameObject.GetComponent<MoveTo>().enabled = false;
            }
            else {
                GameManager.gameManager.buttonInteract.interactable = true;
                GameManager.gameManager.buttonInteract.onClick.RemoveAllListeners();
                GameManager.gameManager.buttonInteract.onClick.AddListener(interact);
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            GameManager.gameManager.buttonInteract.interactable = false;
        }
    }

    void interact()
    {
        GameManager.gameManager.buttonInteract.interactable = false;
        if (isMonarca) {
            if (GameManager.gameManager.isDaggerGift()) {
                DialogueManager.instance.EnqueueDialogue(dialogueAlt);
            } else {
                DialogueManager.instance.EnqueueDialogue(dialogue);
                Inventory.inventory.addItem(itemGift);
                GameManager.gameManager.changeDaggerGift();
                GameManager.gameManager.changeCanExitTown1();
            }
        }

        if (isItemFound) {
            GameManager.gameManager.typing.Stop();
            if (isBayaRojaTown1) {
                if (!GameManager.gameManager.isBayaRojaTown1()) {
                    GameManager.gameManager.itemReceive.Play();
                    GetComponent<CircleCollider2D>().enabled = false;
                    GameManager.gameManager.changeBayaRojaTown1();
                    Inventory.inventory.addItem(itemGift);
                    DialogueManager.instance.EnqueueDialogue(dialogue);
                } else GetComponent<CircleCollider2D>().enabled = false;

            }
            if (isCroquetillaTown1) {
                if (!GameManager.gameManager.isCroquetillaTown1()) {
                    GameManager.gameManager.itemReceive.Play();
                    GetComponent<CircleCollider2D>().enabled = false;
                    GameManager.gameManager.changeCroquetillaTown1();
                    Inventory.inventory.addItem(itemGift);
                    DialogueManager.instance.EnqueueDialogue(dialogue);
                } else GetComponent<CircleCollider2D>().enabled = false;
            }
            if (isCuerdaRuta1) {
                if (!GameManager.gameManager.isCuerdaRuta1()) {
                    GameManager.gameManager.itemReceive.Play();
                    GetComponent<CircleCollider2D>().enabled = false;
                    GameManager.gameManager.changeCuerdaRuta1();
                    Inventory.inventory.addItem(itemGift);
                    DialogueManager.instance.EnqueueDialogue(dialogue);
                }
                else GetComponent<CircleCollider2D>().enabled = false;
            }
            if (isCremaIkigai) {
                if (!GameManager.gameManager.isCremaIkigai()) {
                    GameManager.gameManager.itemReceive.Play();
                    GetComponent<CircleCollider2D>().enabled = false;
                    GameManager.gameManager.changeCremaIkigai();
                    Inventory.inventory.addItem(itemGift);
                    DialogueManager.instance.EnqueueDialogue(dialogue);
                }
                else GetComponent<CircleCollider2D>().enabled = false;
            }
            if (isArmaGod) {
                if (!GameManager.gameManager.isArmaGod()) {
                    GameManager.gameManager.itemReceive.Play();
                    GetComponent<CircleCollider2D>().enabled = false;
                    GameManager.gameManager.changeArmaGod();
                    Inventory.inventory.addItem(itemGift);
                    DialogueManager.instance.EnqueueDialogue(dialogue);
                }
                else GetComponent<CircleCollider2D>().enabled = false;
            }
            if (isOroRuta2) {
                if (!GameManager.gameManager.oroRuta2) {
                    GameManager.gameManager.itemReceive.Play();
                    GetComponent<CircleCollider2D>().enabled = false;
                    GameManager.gameManager.oroRuta2 = true;
                    Inventory.inventory.addItem(itemGift);
                    DialogueManager.instance.EnqueueDialogue(dialogue);
                } else GetComponent<CircleCollider2D>().enabled = false;
            }
        }

        if (isOnlyDialog) {
            if (isEndTown1) {
                if (!GameManager.gameManager.bossDefeated) {
                    DialogueManager.instance.EnqueueDialogue(dialogue);
                    DialogueManager.instance.endDialogueCallback += start3D;
                } else {
                    SceneManager.LoadScene("Ruta2");
                }
            } else {
                DialogueManager.instance.EnqueueDialogue(dialogue);
            }

        }
       
        if (isFishMan) {
            DialogueManager.instance.endDialogueCallback += finishIkigaiScene;
        }

        if (isHealer) {
            DialogueManager.instance.EnqueueDialogue(dialogue);
            foreach(BattleCharacter bc in GameManager.gameManager.saveStats) {
                bc.heal();
            }
        }

    }

    public void finishIkigaiScene()
    {
        GameManager.gameManager.team.Add(GameManager.gameManager.itzieBase);
        GameManager.gameManager.isItzie = false;
        SceneManager.LoadScene("VideoCapitulo3");
    }

    public void start3D() {

        GameManager.gameManager.gameObject.GetComponent<PauseManager>().uiCombateDown();
        GameManager.gameManager.goToScene3D();
    }

    //public override void Interact()
    //{
    //    DialogueManager.instance.EnqueueDialogue(dialogue);
    //    moveTo.enabled = false;
    //    Debug.Log("hola");
    //}
}
