using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestScript : MonoBehaviour
{

    public Quest MyQuest { get; set; }

    private string title = "";
    private Color32 color = new Color32(0, 0, 0, 255);

    private bool markedComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        title = MyQuest.MyTitle;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(color);
        GetComponent<TextMeshProUGUI>().text = title;
        GetComponent<TextMeshProUGUI>().color = color;

    }

    public void Select(){
        color = new Color32(180,4,196,255);
        QuestLog.MyInstance.ShowDescription(MyQuest);
    }

    public void DeSelect(){
        color = new Color32(0,0,0,255);
    }

     public void IsComplete() {
        if (MyQuest.IsComplete() && !markedComplete) {
            Debug.Log("Completed");
            markedComplete = true;
            title = "<s>" + MyQuest.MyTitle + "</s>";
        }else if(!MyQuest.IsComplete()){
            Debug.Log("Hola");
            markedComplete = false;
            title = MyQuest.MyTitle;
        }
    } 

}
