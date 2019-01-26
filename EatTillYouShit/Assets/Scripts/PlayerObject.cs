using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public int playerID;
    private float score = 0;

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
        Debug.Log("Player" + playerID + " scored some points");
        score = data.poopValue;
    }
}
