using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Menu : MonoBehaviour
{


    public Button buttonLoad;
    public GameObject error;
    void Start()
    {
        GameManager.gameManager.gameObject.GetComponent<PauseManager>().uiCombateDown();

    }
    void Update()
    {
        if (buttonLoad != null) {
            buttonLoad.onClick.RemoveAllListeners();
            buttonLoad.onClick.AddListener(loadData);
        }
    }

    public void loadData()
    {
        if (File.Exists(Application.persistentDataPath + "/Player.mjc")) {
            SaveManager.Load();
            Debug.Log(GameManager.gameManager.currentScene + " menu");
            GameManager.gameManager.lastScene = SceneManager.GetActiveScene().name;
            GameManager.gameManager.gameObject.GetComponent<PauseManager>().uiCombateUp();
            GameManager.gameManager.gameObject.GetComponent<PauseManager>().loadGame();
            SceneManager.LoadScene(GameManager.gameManager.currentScene);
        } else if (!GameManager.gameManager.fileExists) {
            StartCoroutine(errorLoading());
        }

    }

    public void newGame()
    {
        SceneManager.LoadScene("VideoInicio");
    }

    public IEnumerator errorLoading()
    {
        buttonLoad.interactable = false;
        error.SetActive(true);
        yield return new WaitForSeconds(1f);
        error.SetActive(false);
        buttonLoad.interactable = true;
    }
}
