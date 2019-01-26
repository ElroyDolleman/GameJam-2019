using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int interval = 6;

    private int eventsReceived = 0;

    private void OnEnable()
    {
        EventManager.StartListening("SCORED_PLAYER1", AddEventReceived);
        EventManager.StartListening("SCORED_PLAYER2", AddEventReceived);
        EventManager.StartListening("SCORED_PLAYER3", AddEventReceived);
        EventManager.StartListening("SCORED_PLAYER4", AddEventReceived);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SCORED_PLAYER1", AddEventReceived);
        EventManager.StopListening("SCORED_PLAYER2", AddEventReceived);
        EventManager.StopListening("SCORED_PLAYER3", AddEventReceived);
        EventManager.StopListening("SCORED_PLAYER4", AddEventReceived);
    }

    private void Update()
    {
        if (Time.frameCount % interval == 0)
        {
            if (eventsReceived >= 4)
            {
                EveryoneDone();
            }
        }
    }

    void AddEventReceived()
    {
        eventsReceived += 1;
        Debug.Log("ScoreManager: eventsReceived is now : " + eventsReceived);
    }

    void EveryoneDone()
    {
        eventsReceived = 0;
        Debug.Log("ScoreManager: event fired \"EVERYONE_DONE\"");
        EventManager.TriggerEvent("EVERYONE_DONE");
    }
}
