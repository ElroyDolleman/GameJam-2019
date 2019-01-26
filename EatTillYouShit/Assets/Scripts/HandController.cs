using System.Collections;
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


        //if ((lastFrame + 1000 <= Time.frameCount)  && (Input.GetButtonDown(playerAction) || GetAxisOnce(playerAction)))
        //{
        //    Debug.Log(lastFrame + " frames " + Time.frameCount);
        //    lastFrame = Time.frameCount;
        //    Debug.Log(playerAction + "triggered");
        //    ////check if food is inside collider
        //    //BoxCollider2D thisCollider =GetComponent<BoxCollider2D>();
        //    //Collider2D[] colls = Physics2D.OverlapBoxAll(transform.position, new Vector2(thisCollider.size.x * transform.localScale.x, thisCollider.size.y * transform.localScale.y), 0);

        //    //foreach (Collider2D coll in colls)
        //    //{
        //    //    Debug.Log(coll.gameObject.name);
        //    //    if (coll.GetComponent<FoodMiniTestje>()) //if (coll.GetComponent<Food>())
        //    //    {
        //    if (foodInteractObject.GetTarget())
        //        player.AddFood(foodInteractObject.GetTarget().GetComponent<Food>());
        //            //break;
        //    //    }
        //    //}
        //}

        //} 

        CheckInput();
    }

    void CheckInput()
    {
        if (GetAxisOnce(playerAction))
        {
            if (foodInteractObject.GetTarget())
            {
                player.AddFood(foodInteractObject.GetTarget().GetComponent<Food>());
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
