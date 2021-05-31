using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest[] quests;

    [SerializeField]
    private QuestLog tmpLog;

    private void Awake(){
        //tmpLog.questScript = new List<QuestScript>();
        //tmpLog.AcceptQuest(quests[0]);
    }

    public void sendQuest(Quest quest)
    {
        tmpLog.AcceptQuest(quest);
        GameManager.gameManager.questList.Add(quest);
    }
}
