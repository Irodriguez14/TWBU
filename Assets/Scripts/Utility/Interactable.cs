using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactRange = 2f;
    public Transform target;
    public bool buttonPressed = false;

    private void Start()
    {
    }

    void Update()
    {
        if (GameManage.instance != null) {
            Debug.Log("hola");
            if (Vector2.Distance(gameObject.transform.position, GameManage.instance.player.position) < interactRange || Vector3.Distance(gameObject.transform.position, target.position) < interactRange) {
                Debug.Log("hola");
                if (Input.GetKeyDown(KeyCode.Z)) {
                    Interact();
                    Debug.Log("interact 1");
                }
            }
        } else {
            Debug.Log("hola");
            if (target != null) {
                Debug.Log("hola");
                if (Vector3.Distance(gameObject.transform.position, target.position) < interactRange) {
                    Debug.Log("hola");
                    if (buttonPressed) {
                        Interact();
                        Debug.Log("interact 2");
                        buttonPressed = false;
                    }
                }
            }
        }
    }
    public virtual void Interact()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
