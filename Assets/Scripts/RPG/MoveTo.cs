using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CodeMonkey.Utils;

public class MoveTo : MonoBehaviour
{

    //[SerializeField] Transform target;
    //private NavMeshAgent agent;
    private FixedJoystick joystick;
    private float horizontal;
    private float vertical;
    [SerializeField] float speed;
    float speedSaved;
    public Rigidbody2D rb;
    public bool isDialogue;

    public Animator anim;

    void Start()
    {
        #region NavMesh
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        //agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
        #endregion
        if (GameManager.gameManager.isItzie) {
            anim.runtimeAnimatorController = Resources.Load("Animation/Itzie") as RuntimeAnimatorController;
        }
        else {
            anim.runtimeAnimatorController = Resources.Load("Animation/Player") as RuntimeAnimatorController;
        }
        joystick = GameManager.gameManager.gameObject.GetComponent<PauseManager>().getJoystick();
        rb = GetComponent<Rigidbody2D>();
        speedSaved = speed;
        if(GameManager.gameManager.lastScene == "Menu") gameObject.transform.position = new Vector3(GameManager.gameManager.position[0], GameManager.gameManager.position[1], GameManager.gameManager.position[2]);
    }


    void Update()
    {
        #region NavMesh
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    agent.SetDestination(new Vector2(mousePosition.x, mousePosition.y));
        //}

        //if (Input.touchCount > 0)
        //{

        //    Touch touch = Input.GetTouch(0);

        //    Vector2 touchPos = touch.position;
        //    Vector2 pos = Camera.main.ScreenToWorldPoint(touchPos);
        //    agent.SetDestination(new Vector2(pos.x, pos.y));

        //}
        #endregion
        if (GameManager.gameManager.isItzie) {
            anim.runtimeAnimatorController = Resources.Load("Animation/Itzie") as RuntimeAnimatorController;
            Debug.Log("adios");
        }
        else {
            anim.runtimeAnimatorController = Resources.Load("Animation/Player") as RuntimeAnimatorController;
            Debug.Log("hola");
        }

        //transform.position += new Vector3(horizontal, vertical, 0) * Time.deltaTime * speed;

    }


    private void FixedUpdate()
    {
        horizontal= joystick.Horizontal;
        vertical= joystick.Vertical;

        rb.MovePosition(new Vector2((transform.position.x + horizontal * speed * Time.deltaTime),
                    transform.position.y + vertical * speed * Time.deltaTime));

        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        if (!rb.IsSleeping()) {
            anim.SetFloat("Speed", 1);
        }
        else {

            anim.SetFloat("Speed", 0);
        }
        Debug.Log(rb.velocity.sqrMagnitude);


    }
    //public NavMeshAgent getAgent() { return this.agent; }


}
