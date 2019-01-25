using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public float sensitivity = 1;
    private int playerID;

    private string playerAxisX;
    private string playerAxisY;
    private string playerAction;

    private Vector3 axisValues;

    private static Camera cam;

    private void Start()
    {
        cam = Camera.main;
        playerID = GetComponent<PlayerObject>().GetPlayerID();
        playerAxisX = "StickXPlayer" + playerID;
        playerAxisY = "StickYPlayer" + playerID;
        playerAction = "ActionPlayer" + playerID;
    }

    private void Update()
    {
        //if(gameRunning){

        //Get converted axis values
        axisValues = new Vector3(Input.GetAxis(playerAxisX) * sensitivity,Input.GetAxis(playerAxisY) * sensitivity, 0);

        //Move the sprite to position
        Vector3 targetPosition = transform.position + axisValues;
        Vector3 viewPosition = cam.WorldToViewportPoint(targetPosition);
        if (viewPosition.x > 1)
        {
            viewPosition = new Vector3(1, viewPosition.y, viewPosition.z);
        }
        else if (viewPosition.x < 0)
        {
            viewPosition = new Vector3(0, viewPosition.y, viewPosition.z);
        }
        if (viewPosition.y > 1)
        {
            viewPosition = new Vector3(viewPosition.x, 1, viewPosition.z);
        }
        else if (viewPosition.y < 0)
        {
            viewPosition = new Vector3(viewPosition.x, 0, viewPosition.z);
        }
        targetPosition = cam.ViewportToWorldPoint(viewPosition);
        transform.position = targetPosition;

        //} 
    }
}
