using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject pointDeath;
    public bool tp = false;

    private void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(GameManager.gameManager.lastScene + " death");
        if (GameManager.gameManager.lastScene == "PruebaTurnBased" && !tp) {
            tp = true;
            Debug.Log(GameManage.instance.player.position);
            GameManage.instance.player.position = pointDeath.transform.position;
            
        }
        
    }
}
