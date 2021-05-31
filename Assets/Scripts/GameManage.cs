
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public static GameManage instance;
    private bool addedCallback1 = false;
    private bool addedCallback2 = false;

    public delegate void tpPlayer();
    public tpPlayer tpPlayerCallback;
    MoveTo moveTo;

    public Transform player;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }

    }

    private void Start()
    {

    }

    private void Update()
    {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player != null) {
                moveTo = player.gameObject.GetComponent<MoveTo>();
                tpPlayerCallback.Invoke();
            }
        }

        if (!addedCallback1) {
            DialogueManager.instance.enableMoveCallback += enableMove;
            addedCallback1 = true;
        }
        if (!addedCallback2) {
            DialogueManager.instance.disableMoveCallback += disableMove;
            addedCallback2 = true;
        }
    }

    public void enableMove()
    {
        if (moveTo != null && player != null) {
            GameManager.gameManager.typing.Stop();
            //moveTo.enabled = true;
            //moveTo.getAgent().isStopped = false;
            GameManager.gameManager.buttonInteract.interactable = false;
            if (GameManager.gameManager.mapa != null) {
                GameManager.gameManager.mapa.SetActive(true);
            }
            instance.player.gameObject.GetComponent<MoveTo>().enabled = true;
            GameManager.gameManager.enableRandom = true;
            instance.player.gameObject.GetComponent<MoveTo>().enabled = true;
            GameManager.gameManager.gameObject.GetComponent<PauseManager>().enableJoystick();
            GameManager.gameManager.gameObject.GetComponent<PauseManager>().enablePause();
        }
    }
    public void disableMove()
    {
        if (moveTo != null && player != null) {
            moveTo.isDialogue = true;
            GameManager.gameManager.enableRandom = false;
            GameManager.gameManager.buttonInteract.interactable = false;
            if (GameManager.gameManager.mapa != null) {
                GameManager.gameManager.mapa.SetActive(false);
            }
            instance.player.gameObject.GetComponent<MoveTo>().anim.SetFloat("Horizontal", 0);
            instance.player.gameObject.GetComponent<MoveTo>().anim.SetFloat("Vertical", 0);
            instance.player.gameObject.GetComponent<MoveTo>().anim.SetFloat("Speed", 0);
            instance.player.gameObject.GetComponent<MoveTo>().enabled = false;
            GameManager.gameManager.gameObject.GetComponent<PauseManager>().disableJoystick();
            Input.ResetInputAxes();
            GameManager.gameManager.gameObject.GetComponent<PauseManager>().disablePause();
        }
    }

}
