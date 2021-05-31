using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string nameScene;
    [SerializeField] bool isVyHouse;
    [SerializeField] bool isOutside;
    [SerializeField] bool isEndGame;

    [Header("Exit town 1")]
    [SerializeField] bool isExitTown1;
    [SerializeField] DialogueBase dialogue;

    private void Start()
    {
        GameManager.gameManager.buttonInteract.interactable = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            if (isExitTown1) {
                if (!GameManager.gameManager.canExitTown1()) {
                    GameManager.gameManager.buttonInteract.interactable = true;
                    //GameManager.gameManager.buttonInteract.gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
                    GameManager.gameManager.buttonInteract.onClick.RemoveAllListeners();
                    GameManager.gameManager.buttonInteract.onClick.AddListener(enqueueDialog);
                    
                } else {
                    GameManager.gameManager.buttonInteract.interactable = true;
                    GameManager.gameManager.buttonInteract.onClick.RemoveAllListeners();
                    GameManager.gameManager.buttonInteract.onClick.AddListener(changeScene);
                }
            } else {
                if (other.gameObject.tag == "Player") {
                    if (isOutside) {
                        GameManager.gameManager.lastScene = SceneManager.GetActiveScene().name;
                        SceneManager.LoadScene(nameScene);
                    } else {
                    GameManager.gameManager.buttonInteract.interactable = true;
                    GameManager.gameManager.buttonInteract.onClick.RemoveAllListeners();
                    GameManager.gameManager.buttonInteract.onClick.AddListener(changeScene);
                    }
                }
            }



        }

    }

    public void enqueueDialog()
    {
        DialogueManager.instance.EnqueueDialogue(dialogue);
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            GameManager.gameManager.buttonInteract.interactable = false;
        }
    }

    public void changeScene()
    {
        GameManager.gameManager.lastScene = SceneManager.GetActiveScene().name;
        if (!isEndGame) {
            SceneManager.LoadScene(nameScene);
        } else {
            SaveManager.SavePlayer();
            GameManager.gameManager.gameObject.GetComponent<PauseManager>().uiCombateDown();
            SceneManager.LoadScene(nameScene);
        }

    }
}
