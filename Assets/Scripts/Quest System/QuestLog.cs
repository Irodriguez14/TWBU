using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

[System.Serializable]
public class QuestLog : MonoBehaviour
{
    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questParent;

    [SerializeField]
    public TextMeshProUGUI questDescription;
    [SerializeField]
    public TextMeshProUGUI questDescriptionTitle;
    [SerializeField]
    public TextMeshProUGUI questObjectivesTitle;
    [SerializeField]
    public TextMeshProUGUI questObjectivesList;

    private static QuestLog instance;

    private Quest selected;

    public List<QuestScript> questScript;

    public static QuestLog MyInstance {
        get {
            if (instance == null) {
                instance = GameManager.gameManager.questLog;
                Debug.Log(instance);
            }

            return instance;
        }
    }

    private void Awake() {
       
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(selected != null) {
            foreach (CollectObjective o in selected.CollectObjectives) {
                o.UpdateItemCount(null);
            }
        }
        
    }

    public void AcceptQuest(Quest quest){
        GameObject fekts = Instantiate(questPrefab, questParent);
        selected = quest;
        QuestScript qs = fekts.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;
        qs.MyQuest = quest;
        questScript.Add(qs);
        
        foreach (CollectObjective o in quest.CollectObjectives)
        {
            o.UpdateItemCount(null);
        }
        fekts.GetComponent<TextMeshProUGUI>().text = quest.MyTitle + "\n";
    }

    public void UpdateSelected(){
        ShowDescription(selected);
    }

    public void ShowDescription(Quest quest){
        if (selected == null)
        {
            Debug.Log(selected.MyQuestScript);
            selected.MyQuestScript.DeSelect();
        }


        string objectives = string.Empty;
        string title = quest.MyTitle;
        string description = quest.MyDescription;

        foreach (Objective obj in quest.CollectObjectives)
        {
            objectives += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
        }
        questDescription.text = description;
        questDescriptionTitle.text = title;
        if (!quest.showObjectives) {
            questObjectivesTitle.text = "";
            questObjectivesList.text = "";
        } else {
            questObjectivesTitle.text = "Objetivos";
            questObjectivesList.text = objectives;
        }

        //questDescription.text = string.Format("{0}\n<size=15>{1}</size>\nObjectives\n{2}", title,description,objectives);
    }
    public bool CheckCompletion(){
        foreach (QuestScript qs in questScript)
        {
            qs.IsComplete();
            return true;
        }
        return false;
    }
}
