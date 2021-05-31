using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] Canvas normalCanvas;
    [SerializeField] GameObject gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    public void gameOver()
    {
        normalCanvas.gameObject.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameOverCanvas.gameObject.GetComponent<Animator>().SetTrigger("GameOver");

    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
