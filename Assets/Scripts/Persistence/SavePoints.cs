using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoints : MonoBehaviour
{
    [Header("Pueblo 1")]
    [SerializeField] GameObject vyHouse;
    [SerializeField] GameObject house1;
    [SerializeField] GameObject house2;
    [SerializeField] GameObject exitTown1;
    [SerializeField] GameObject backToRuta1;
    [SerializeField] GameObject cap3;

    private void Start()
    {
        GameManager.gameManager.gameObject.GetComponent<PauseManager>().closePause();

        GameManage.instance.tpPlayerCallback = null;
        GameManage.instance.tpPlayerCallback += makeTP;

    }

    public void makeTP()
    {
        if (GameManager.gameManager.lastScene == "CasaTown1_1") {
            GameManage.instance.player.position = house1.transform.position;
        }
        if (GameManager.gameManager.lastScene == "CasaTown1_2") {
            GameManage.instance.player.position = house2.transform.position;
        }
        if (GameManager.gameManager.lastScene == "Ruta1") {
            GameManage.instance.player.position = exitTown1.transform.position;
        }
        if (GameManager.gameManager.lastScene == "PruebaTurnBased") {
            GameManage.instance.player.position = GameManager.gameManager.positionBeforeCombat;
        }
        if (GameManager.gameManager.lastScene == "Ruta2") {
            GameManage.instance.player.position = backToRuta1.transform.position;
        }
        if (GameManager.gameManager.lastScene == "VideoCapitulo3") {
            GameManage.instance.player.position = cap3.transform.position;
        }
        if (GameManager.gameManager.lastScene == "Menu") {
            GameManage.instance.player.position = new Vector3(GameManager.gameManager.position[0], GameManager.gameManager.position[1], GameManager.gameManager.position[2]);
        }
    }

}
