using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int interval = 6;
    public float decisionTime = 5;
    public float waitBetweenDishesTime = 3;

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
    List<PlayerObject> playerObjectList;

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

    private void Start()
    {
        playerObjectList = new List<PlayerObject>();
        playerObjectList.Add(player1.GetComponent<PlayerObject>());
        playerObjectList.Add(player2.GetComponent<PlayerObject>());
        playerObjectList.Add(player3.GetComponent<PlayerObject>());
        playerObjectList.Add(player4.GetComponent<PlayerObject>());
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
            if (!timerStarted)
            {
                Debug.Log("ScoreManager: timer started");
                timerStarted = true;
                startTime = Time.time;
            }
            //when timer = 0
            //Debug.Log("ScoreManager: is time passed? " + ((startTime + decisionTime) == Time.time));
            if (Time.time >= (startTime + decisionTime)) {
            //if (startTime > 5f) { 
                Food target = null;
                foreach (Food f in DishManager.instance.GetCurrentFood())
                {
                    if (!f.isTaken)
                    {
                        target = f;
                    }
                }

                if (!p1Received)
                {
                    StartCoroutine(player1.GetComponent<HandController>().Automatic(target));
                }
                else if (!p2Received)
                {
                    StartCoroutine(player2.GetComponent<HandController>().Automatic(target));
                }
                else if (!p3Received)
                {
                    StartCoroutine(player3.GetComponent<HandController>().Automatic(target));
                }
                else if (!p4Received)
                {
                    StartCoroutine(player4.GetComponent<HandController>().Automatic(target));
                }
                //}
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
        p1Received = false;
        p2Received = false;
        p3Received = false;
        p4Received = false;

        for (int i = 0; i < playerObjectList.Count; i++)
        {
            playerObjectList[i].FillPoopMeter();
        }

        StartCoroutine(WaitForNextDish());
        //Debug.Log("ScoreManager: event fired \"EVERYONE_DONE\"");
        //EventManager.TriggerEvent("EVERYONE_DONE");
    }

    IEnumerator WaitForNextDish()
    {
        yield return new WaitForSeconds(waitBetweenDishesTime);
        DishManager.instance.ResetDish();
        EventManager.TriggerEvent("EVERYONE_DONE");
    }
}
