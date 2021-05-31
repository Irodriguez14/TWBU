using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestsSaveFile 
{
    public string title;
    public string description;
    public bool showObjectives;

    public int amount;
    public string type;
    public bool eraseItem;

    public QuestsSaveFile(string title, string description, 
        bool showObjectives, int amount, string type, bool eraseItem) {
        this.title = title;
        this.description = description;
        this.showObjectives = showObjectives;
        this.amount = amount;
        this.type = type;
        this.eraseItem = eraseItem;
    }
}
