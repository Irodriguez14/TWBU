using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterBeginGame : MonoBehaviour
{
    public DialogueBase dialogue;
    [SerializeField] ItemBase itemToGive;
    [SerializeField] Quest quest;

    [SerializeField] QuestLog questList;

    void Start()
    {
        StartCoroutine(waitToDialog());
    }
    
    void Update()
    {
        
    }

    IEnumerator waitToDialog()
    {
        yield return new WaitForSeconds(0.5f);
        if (!GameManager.gameManager.letterComplete()) {
            DialogueManager.instance.EnqueueDialogue(dialogue);
            Inventory.inventory.addItem(itemToGive);
            Inventory.inventory.addItem(itemToGive);
            Debug.Log(GameManager.gameManager.quests);
            GameManager.gameManager.quests.sendQuest(quest);
        }
        GameManager.gameManager.readLetter();
    }
}
