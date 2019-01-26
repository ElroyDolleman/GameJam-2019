using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int interval = 6;
    public float decisionTime = 5;

    private int eventsReceived = 0;

    private bool timerStarted = false;
    private float startTime = 0;

    private bool p1Received = false;
    private bool p2Received = false;
    private bool p3Received = false;
    private bool p4Received = false;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private void OnEnable()
    {
        EventManager.StartListening("SCORED_PLAYER1", AddReceivePlayer1);
        EventManager.StartListening("SCORED_PLAYER2", AddReceivePlayer2);
        EventManager.StartListening("SCORED_PLAYER3", AddReceivePlayer3);
        EventManager.StartListening("SCORED_PLAYER4", AddReceivePlayer4);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SCORED_PLAYER1", AddReceivePlayer1);
        EventManager.StopListening("SCORED_PLAYER2", AddReceivePlayer2);
        EventManager.StopListening("SCORED_PLAYER3", AddReceivePlayer3);
        EventManager.StopListening("SCORED_PLAYER4", AddReceivePlayer4);
    }

    private void Update()
    {
        //if (Time.frameCount % interval == 0)
        //{
            if (eventsReceived >= 4)
            {
                EveryoneDone();
            }
            else if (eventsReceived >= 1)
            {
                //start timer;
                //if (!timerStarted)
                //{
                //    Debug.Log("ScoreManager: timer started");
                //    timerStarted = true;
                //    startTime = Time.time;
                //}
                startTime += Time.deltaTime;
                //when timer = 0
                Debug.Log("ScoreManager: is time passed? " + ((startTime + decisionTime) == Time.time));
                //if (Time.time == (startTime + decisionTime)) {
                if (startTime > 5f) { 
                    Food target = null;
                    foreach (Food f in FindObjectsOfType<Food>())
                    {
                        if (!f.isTaken)
                        {
                            target = f;
                        }
                    }
                    if (!p1Received)
                    {
                        player1.GetComponent<HandController>().TakeFood(target);
                    } else if (!p2Received)
                    {
                        player2.GetComponent<HandController>().TakeFood(target);
                    }
                    else if (!p3Received)
                    {
                        player3.GetComponent<HandController>().TakeFood(target);
                    }
                    else if (!p4Received)
                    {
                        player4.GetComponent<HandController>().TakeFood(target);
                    }
                }
            }
        //}
    }

    void AddReceivePlayer1()
    {
        p1Received = true;
        eventsReceived += 1;
        Debug.Log("ScoreManager: eventsReceived is now : " + eventsReceived);
    }

    void AddReceivePlayer2()
    {
        p2Received = true;
        eventsReceived += 1;
        Debug.Log("ScoreManager: eventsReceived is now : " + eventsReceived);
    }

    void AddReceivePlayer3()
    {
        p3Received = true;
        eventsReceived += 1;
        Debug.Log("ScoreManager: eventsReceived is now : " + eventsReceived);
    }

    void AddReceivePlayer4()
    {
        p4Received = true;
        eventsReceived += 1;
        Debug.Log("ScoreManager: eventsReceived is now : " + eventsReceived);
    }

    void AddEventReceived()
    {
        eventsReceived += 1;
        Debug.Log("ScoreManager: eventsReceived is now : " + eventsReceived);
    }

    void EveryoneDone()
    {
        eventsReceived = 0;
        timerStarted = false;
        Debug.Log("ScoreManager: event fired \"EVERYONE_DONE\"");
        EventManager.TriggerEvent("EVERYONE_DONE");
    }
}
