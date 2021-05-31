using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField]
    private string title;

    [SerializeField]
    private string description;
    [SerializeField]
    public List<CollectObjective> collectObjectives;
    public bool showObjectives;

    [SerializeField]
    public QuestScript MyQuestScript { get; set; }

    public Quest(string title, string description, CollectObjective collectObjectives, bool showObjectives) {
        this.title = title;
        this.description = description;
        this.collectObjectives = new List<CollectObjective>();
        this.collectObjectives.Add(collectObjectives);
        this.showObjectives = showObjectives;
    }

    public string MyTitle{
        get{
            return title;
        }
        set{
            title = value;
        }
    }

    public string MyDescription
    {
        get
        {
            return description;
        }
        set
        {
            description = value;
        }
    }

    public List<CollectObjective> CollectObjectives { get => collectObjectives; }

     public bool IsComplete(){
            foreach(Objective o in collectObjectives){
                if (!o.IsComplete)
                {
                    return false;
                }
            return true;
            }
        return false;
    }
}


[System.Serializable]
public abstract class Objective {

    [SerializeField]
    public int amount;

    private int currentAmount;

    [SerializeField]
    public string type;

    public int MyAmount { get => amount; }
    public int MyCurrentAmount { get => currentAmount; set => currentAmount = value; }
    public string MyType { get => type; }

    public bool IsComplete{
        get{
            return MyCurrentAmount >= MyAmount;
        }
    }
}

[System.Serializable]
public class CollectObjective : Objective
{
    public bool eraseItems;

    public CollectObjective(bool eraseItems, string type, int amount) {
        this.eraseItems = eraseItems;
        this.type = type;
        this.amount = amount;
    }

    public void UpdateItemCount(Item item){
        for (int i = 0; i < Inventory.inventory.items.Count; i++)
        {
            if(MyType.Equals(Inventory.inventory.items[i].item.getItemName())){
                MyCurrentAmount = Inventory.inventory.items[i].amount;
                if(QuestLog.MyInstance.CheckCompletion()) {
                    //QuestLog.MyInstance.UpdateSelected();
                    if (eraseItems) {
                        Inventory.inventory.removeItem(Inventory.inventory.items[i]);
                    }
                }
                Debug.Log(MyCurrentAmount);
            }
        } 
    }
}