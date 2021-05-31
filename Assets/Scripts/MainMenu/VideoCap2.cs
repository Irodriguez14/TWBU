using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoCap2 : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    public string sceneName;
    public bool setItzie;
    public bool isFirst;

    private void Awake() {
        GameManager.gameManager.gameObject.GetComponent<PauseManager>().uiCombateDown();
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer video) {
        GameManager.gameManager.lastScene = SceneManager.GetActiveScene().name;
        GameManager.gameManager.gameObject.GetComponent<PauseManager>().uiCombateUp();
        SceneManager.LoadScene(sceneName);
        if(setItzie) GameManager.gameManager.setItzie();
        if (isFirst) {
            GameManager.gameManager.gameObject.GetComponent<PauseManager>().setupPause();
            GameManager.gameManager.gameObject.GetComponent<PauseManager>().uiCombateUp();
        }
    }
}
