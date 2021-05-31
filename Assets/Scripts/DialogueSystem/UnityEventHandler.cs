using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnityEventHandler : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent eventHandler;
    public DialogueBase myDialogue;


    //esto es lo que pasa cuando le das click a este button
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        eventHandler.Invoke();
        DialogueManager.instance.CloseOptions();

        if(myDialogue != null)
        {
            DialogueManager.instance.disableMoveCallback.Invoke();
            DialogueManager.instance.EnqueueDialogue(myDialogue);
        }
        else {
            DialogueManager.instance.enableMoveCallback.Invoke();
        }
    }
}
