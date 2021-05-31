using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public delegate void enableMove();
    public enableMove enableMoveCallback;
    public delegate void disableMove();
    public disableMove disableMoveCallback;
    public delegate void endDialogue();
    public endDialogue endDialogueCallback;
    public static DialogueManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Fix this" + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    public Image dialoguePortrait;
    public GameObject panel;
    public GameObject namePanel;
    public float delay = 0.001f;

    public Queue<DialogueBase.Info> dialogueInfo = new Queue<DialogueBase.Info>(); //coleccion FIFO

    //options
    private bool isDialogueOption;
    public GameObject dialogueOptionUI;
    private bool inDialogue;
    public GameObject[] optionButtons;
    private int optionsAmount;
    public Text questionText;

    public void EnqueueDialogue(DialogueBase db)
    {
        if (inDialogue) return;
        inDialogue = true;

        dialogueBox.SetActive(true);
        dialogueInfo.Clear();
        disableMoveCallback.Invoke();

        if (db is DialogueOptions)
        {
            isDialogueOption = true;
            DialogueOptions dialogueOptions = db as DialogueOptions;
            optionsAmount = dialogueOptions.optionsInfo.Length;
            questionText.text = dialogueOptions.questionText;

            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].SetActive(false);
            }

            for (int i = 0; i < optionsAmount; i++)
            {
                optionButtons[i].SetActive(true);
                optionButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dialogueOptions.optionsInfo[i].buttonName;
                UnityEventHandler myEventHandler = optionButtons[i].GetComponent<UnityEventHandler>();
                myEventHandler.eventHandler = dialogueOptions.optionsInfo[i].myEvent;
                if(dialogueOptions.optionsInfo[i].nextDialogue != null)
                {
                    myEventHandler.myDialogue = dialogueOptions.optionsInfo[i].nextDialogue;
                }
                else
                {
                    myEventHandler.myDialogue = null;
                }
            }
        }
        else
        {
            isDialogueOption = false;
        }

        foreach(DialogueBase.Info info in db.dialogueInfo)
        { 
            dialogueInfo.Enqueue(info);
        }
        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
        if(dialogueInfo.Count == 0)
        {
            EndofDialogue();
            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();

        dialogueName.text = info.myName;
        dialogueText.text = info.myText;
        if(info.myName == null) {
            panel.SetActive(false);
        } else {
            panel.SetActive(true);
        }

        if(info.portrait == null){
            dialoguePortrait.color = new Color32(0, 0, 0, 0);
        } else {
            dialoguePortrait.sprite = info.portrait;
            dialoguePortrait.color = new Color32(255, 255, 255, 255);
        }

        StartCoroutine(TypeText(info));

    }

    IEnumerator TypeText(DialogueBase.Info info)
    {
        panel.SetActive(false);
        dialogueText.text = "";
        GameManager.gameManager.typing.Play();
        foreach (char c in info.myText.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
            yield return null;
        }
        panel.SetActive(true);
        GameManager.gameManager.typing.Stop();
    }

    public void EndofDialogue()
    {
        dialogueBox.SetActive(false);
        inDialogue = false;
        OptionsLogic();
    }

    private void OptionsLogic()
    {
        if (isDialogueOption == true)
        {
            disableMoveCallback.Invoke();
            dialogueOptionUI.SetActive(true);
        }
        else {
            enableMoveCallback.Invoke();
            if(endDialogueCallback != null) endDialogueCallback.Invoke();
        }        
    }

    public void CloseOptions()
    {
        dialogueOptionUI.SetActive(false);
    }

   
}
