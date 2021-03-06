﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public int playerID;
    public PoopMeter poopMeter;
    private int score = 0;

    public float GetScore()
    {
        return score;
    }

    public int GetPlayerID()
    {
        return playerID;
    }

    public void AddFood(Food data)
    {
        Debug.Log("Player --" + data.name + "scorevalue " + data.poopValue);
        EventManager.TriggerEvent("SCORED_PLAYER" + playerID);
        score = data.poopValue;
    }

    public void FillPoopMeter()
    {
        poopMeter.AddPoop(score);
    }
}
