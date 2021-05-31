using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToMove : MonoBehaviour
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/
    private Vector2 target;
    // Update is called once per frame
    void Update()
    {

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            target = new Vector2(mousePosition.x, mousePosition.y);
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * 1000f);
        }

        if (Input.touchCount > 0) {

            Touch touch = Input.GetTouch(0);

            Vector3 touchPos = touch.position;
            Transform camTrans = Camera.main.transform;
            float dist = Vector3.Dot(transform.position - camTrans.position, camTrans.forward);
            touchPos.z = dist;
            Vector3 pos = Camera.main.ScreenToWorldPoint(touchPos);

            Vector3 tmp = transform.position;
            tmp.x = pos.x;
            tmp.y = pos.y;
            transform.position = tmp;

            //navMeshAgent.SetDestination(target);

        }


    }
}
