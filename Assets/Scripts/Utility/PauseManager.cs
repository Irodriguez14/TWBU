using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject skills;
    [SerializeField] GameObject misions;
    [SerializeField] GameObject options;
    [SerializeField] GameObject blurPanel;
    [SerializeField] GameObject division;

    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject unPauseButton;
    [SerializeField] List<TextMeshProUGUI> titles;
    [SerializeField] List<TextMeshProUGUI> titlesColor;

    [SerializeField] FixedJoystick joystick;
    private GameObject handle;

    public delegate void enableMove();
    public enableMove enableMoveCallback;
    public delegate void disableMove();
    public disableMove disableMoveCallback;

    private void Start()
    {
        inventory.SetActive(false);
        misions.SetActive(false);
        skills.SetActive(false);
        options.SetActive(false);
        pauseButton.gameObject.GetComponent<Button>().onClick.AddListener(hitPause);
        pauseButton.SetActive(false);
        division.SetActive(false);
        unPauseButton.SetActive(false);
        foreach (TextMeshProUGUI text in titles) {
            text.gameObject.SetActive(false);
        }
        handle = joystick.gameObject.transform.GetChild(0).gameObject;
        disableJoystick();
    }

    public void uiCombateDown()
    {
        inventory.SetActive(false);
        misions.SetActive(false);
        skills.SetActive(false);
        options.SetActive(false);
        pauseButton.SetActive(false);
        division.SetActive(false);
        unPauseButton.SetActive(false);
        foreach (TextMeshProUGUI text in titles) {
            text.gameObject.SetActive(false);
        }
        joystick.gameObject.SetActive(false);
        GameManager.gameManager.buttonInteract.gameObject.SetActive(false);
        if (GameManager.gameManager.mapa != null) {
            GameManager.gameManager.mapa.SetActive(false);
        }
    }

    public void uiCombateUp()
    {
        inventory.SetActive(false);
        misions.SetActive(false);
        skills.SetActive(false);
        options.SetActive(false);
        pauseButton.SetActive(true);
        pauseButton.GetComponent<Button>().interactable = true;
        division.SetActive(false);
        unPauseButton.SetActive(false);
        foreach (TextMeshProUGUI text in titles) {
            text.gameObject.SetActive(false);
        }
        joystick.gameObject.SetActive(true);
        enableJoystick();
        GameManager.gameManager.buttonInteract.gameObject.SetActive(true);
        if (GameManager.gameManager.mapa != null) {
            GameManager.gameManager.mapa.SetActive(true);
        }
    }

    public void hitPause()
    {
        if(Inventory.inventory.onItemChangedCallback != null) Inventory.inventory.onItemChangedCallback.Invoke()
         ;
        GameManager.gameManager.enableRandom = false;
        DialogueManager.instance.disableMoveCallback.Invoke();
        pauseButton.SetActive(false);
        unPauseButton.SetActive(true);
        blurPanel.SetActive(true);
        showInventory();
        foreach (TextMeshProUGUI text in titles) {
            text.gameObject.SetActive(true);
        }
        division.SetActive(true);
        disableJoystick();
        if (GameManager.gameManager.mapa != null) {
            GameManager.gameManager.mapa.SetActive(false);
        }
    }

    public void setupPause()
    {
        blurPanel.SetActive(false);
        pauseButton.SetActive(true);
        unPauseButton.SetActive(false);
        foreach (TextMeshProUGUI text in titles) {
            text.gameObject.SetActive(false);
        }
        division.SetActive(false);
        disableJoystick();
        if (GameManager.gameManager.mapa != null) {
            GameManager.gameManager.mapa.SetActive(false);
        }
    }

    public void closePause()
    {
        GameManager.gameManager.enableRandom = true;
        DialogueManager.instance.enableMoveCallback.Invoke();
        blurPanel.SetActive(false);
        pauseButton.SetActive(true);
        unPauseButton.SetActive(false);
        foreach (TextMeshProUGUI text in titles) {
            text.gameObject.SetActive(false);
        }
        inventory.SetActive(false);
        misions.SetActive(false);
        skills.SetActive(false);
        options.SetActive(false);
        division.SetActive(false);
        enableJoystick();
        if (GameManager.gameManager.mapa != null) {
            GameManager.gameManager.mapa.SetActive(true);
        }
    }

    public void disablePause() { pauseButton.GetComponent<Button>().interactable = false; }
    public void enablePause() { pauseButton.GetComponent<Button>().interactable = true; }

    public void disableJoystick(){
        joystick.enabled = false;
        handle.transform.localPosition = new Vector3(0, 0, 0);
    }
    public void enableJoystick() {
        joystick.enabled = true;
        handle.transform.localPosition = new Vector3(0, 0, 0);
    }
    public FixedJoystick getJoystick() { return this.joystick; }

    public void showInventory()
    {
        inventory.SetActive(true);
        misions.SetActive(false);
        skills.SetActive(false);
        options.SetActive(false);
        titlesColor[0].color = new Color32(244, 156, 12, 255);
        titlesColor[1].color = new Color32(255, 255, 255, 255);
        titlesColor[2].color = new Color32(255, 255, 255, 255);
        titlesColor[3].color = new Color32(255, 255, 255, 255);
    }
    public void showSkills()
    {
        inventory.SetActive(false);
        misions.SetActive(false);
        skills.SetActive(true);
        options.SetActive(false);
        titlesColor[0].color = new Color32(255, 255, 255, 255);
        titlesColor[1].color = new Color32(244, 156, 12, 255);
        titlesColor[2].color = new Color32(255, 255, 255, 255);
        titlesColor[3].color = new Color32(255, 255, 255, 255);
    }
    public void showMisions()
    {
        inventory.SetActive(false);
        misions.SetActive(true);
        skills.SetActive(false);
        options.SetActive(false);
        titlesColor[0].color = new Color32(255, 255, 255, 255);
        titlesColor[1].color = new Color32(255, 255, 255, 255);
        titlesColor[2].color = new Color32(244, 156, 12, 255);
        titlesColor[3].color = new Color32(255, 255, 255, 255);
    }
    public void showOptions()
    {
        inventory.SetActive(false);
        misions.SetActive(false);
        skills.SetActive(false);
        options.SetActive(true);
        titlesColor[0].color = new Color32(255, 255, 255, 255);
        titlesColor[1].color = new Color32(255, 255, 255, 255);
        titlesColor[2].color = new Color32(255, 255, 255, 255);
        titlesColor[3].color = new Color32(244, 156, 12, 255);
    }

    public void loadGame()
    {
        inventory.SetActive(false);
        misions.SetActive(false);
        skills.SetActive(false);
        options.SetActive(false);
        pauseButton.SetActive(true);
        enablePause();
        division.SetActive(false);
        unPauseButton.SetActive(false);
        foreach (TextMeshProUGUI text in titles) {
            text.gameObject.SetActive(false);
        }
        handle = joystick.gameObject.transform.GetChild(0).gameObject;
        enableJoystick();
        DialogueManager.instance.dialogueBox.SetActive(false);
    }

}
