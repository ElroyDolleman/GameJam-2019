using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public int playerID;
    private int score = 0;

    public int GetScore()
    {
        return score;
    }

    public int GetPlayerID()
    {
        return playerID;
    }
}
