using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject gameManager;


    public void startGame()
    {
        SceneManager.LoadScene("VideoCapitulo1");
    }

}
