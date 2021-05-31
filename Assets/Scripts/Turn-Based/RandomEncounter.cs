using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomEncounter : MonoBehaviour
{
    [SerializeField] float CD = 1f;
    private float actualCD;


    void Start()
    {
        actualCD = CD;
        
    }
    
    void Update()
    {
        if (GameManager.gameManager.enableRandom) {
            actualCD -= Time.deltaTime;
            if (actualCD <= 0) {
                if (Random.Range(1, 101) <= 10) {
                    GameManager.gameManager.sceneBeforeCombat = SceneManager.GetActiveScene().name;
                    GameManager.gameManager.positionBeforeCombat = GameManage.instance.player.position;
                    GameManager.gameManager.gameObject.GetComponent<PauseManager>().uiCombateDown();
                    SceneManager.LoadScene("PruebaTurnBased");
                }
                else {
                    actualCD = CD;
                }
            }
        }
        

        
    }
}
