﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public float movementSensitivity = 1;
    public float actionSensitivity = 0.8f;
    private PlayerObject player;
    private int playerID;

    private string playerAxisX;
    private string playerAxisY;
    private string playerAction;

    private Vector3 axisValues;
    private bool lastAxisState;

    private static Camera cam;
    private FoodInteract foodInteractObject;

    private int lastFrame = 0;
    private bool scored = false;

    private void Start()
    {
        cam = Camera.main;
        playerID = GetComponent<PlayerObject>().GetPlayerID();
        playerAxisX = "StickXPlayer" + playerID;
        playerAxisY = "StickYPlayer" + playerID;
        playerAction = "ActionPlayer" + playerID;
        player = GetComponent<PlayerObject>();
        foodInteractObject = GetComponent<FoodInteract>();
    }

    private void Update()
    {
        //if(gameRunning){
        //Debug.Log("FrameCount: " + Time.frameCount + " at object: " + this.name);
        //Get converted axis values
        axisValues = new Vector3(Input.GetAxis(playerAxisX) * movementSensitivity,Input.GetAxis(playerAxisY) * movementSensitivity, 0);

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

        if (!scored)
        {
            CheckInput();
        }

        //}
    }

    void CheckInput()
    {
        if (GetAxisOnce(playerAction))
        {
            Food target = foodInteractObject.GetTarget();
            if (target != null && !target.isTaken)
            {
                //player.AddFood(foodInteractObject.GetTarget().GetComponent<Food>());
                player.AddFood(target);
                scored = true;
            }
        }
    }

    bool GetAxisOnce(string axisName)
    {
        bool currentAxisState = Input.GetAxis(axisName) > actionSensitivity;

        if (currentAxisState && lastAxisState)
        {
            return false;
        }

        lastAxisState = currentAxisState;

        return currentAxisState;
    }
}
