using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class EventBehaviour : ScriptableObject
{
    public void TestEvent()
    {
        Debug.Log("Test Event Successfull");

    }

    public void TestEvent2()
    {
        Debug.Log("Test Event Successfull2");

    }

    public void TestEvent3()
    {
        Debug.Log("Test Event Successfull3");

    }
}
