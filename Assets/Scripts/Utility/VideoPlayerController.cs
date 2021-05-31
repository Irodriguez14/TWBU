using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerController : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameStart gameController;

    private void Awake()
    {
        videoPlayer.loopPointReached += EndReached;
        GameManager.gameManager.buttonInteract.gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        GameManager.gameManager.gameObject.GetComponent<PauseManager>().getJoystick().gameObject.SetActive(false);
    }

    void EndReached(VideoPlayer video)
    {
        Debug.Log("Finished");
        GameManager.gameManager.buttonInteract.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        GameManager.gameManager.gameObject.GetComponent<PauseManager>().getJoystick().gameObject.SetActive(true);
        gameController.startGame();
    }
}
