using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoFinal : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    private void Awake() {
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer video) {
        GameManager.gameManager.gameObject.GetComponent<PauseManager>().uiCombateUp();
        SceneManager.LoadScene("Menu");

    }
}
