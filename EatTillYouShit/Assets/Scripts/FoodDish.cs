﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDish : MonoBehaviour
{
    public enum DishStates
    {
        wait,
        serve,
        anticipation,
        open,
        remove,
    }

    DishStates currentState;
    float easeStep = 0;
    float yPosStateChange = 0; // The y position when the state changed

    public float startPosY = 7;
    public float endPosY { get { return -startPosY; } }

    List<Food> foodList;

    private void OnValidate()
    {
        startPosY = transform.position.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosY = transform.position.y;

        foodList = new List<Food>();

        ResetDish();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case DishStates.serve:
                Serving();
                break;
            case DishStates.remove:
                Removing();
                break;
        }
    }

    void Serving()
    {
        if (MoveTo(0))
        {
            ChangeState(DishStates.anticipation);
        }
    }

    void Removing()
    {
        // Don't move to end position the first time
        if (transform.position.y == startPosY)
        {
            ChangeState(DishStates.wait);
            return;
        }

        if (MoveTo(endPosY))
        {
            ChangeState(DishStates.wait);
            ResetDish();
        }
    }

    bool MoveTo(float moveToY)
    {
        easeStep = Mathf.Min(1, easeStep + Time.deltaTime);
        
        float ypos = Easing.EaseOutQuint(yPosStateChange, moveToY, easeStep);

        ChangeYPos(ypos);

        return easeStep >= 1;
    }

    private void ResetDish()
    {
        ChangeYPos(startPosY);

        DestroyOldFood();
        RandomizeFood();
    }

    void DestroyOldFood()
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            GameObject.Destroy(foodList[i].gameObject);
            foodList.RemoveAt(i);
            i--;
        }
    }

    void RandomizeFood()
    {
        Food foodPrefab = FoodManager.instance.GetRandomFood(MenuTypes.starters);

        Food newFood = GameObject.Instantiate(foodPrefab);

        newFood.transform.SetParent(transform);
        newFood.transform.localPosition = Vector3.zero;

        foodList.Add(newFood);
    }

    void ChangeYPos(float y)
    {
        var pos = transform.position;
        pos.y = y;
        transform.position = pos;
    }

    public void ChangeState(DishStates newState)
    {
        currentState = newState;

        easeStep = 0;
        yPosStateChange = transform.position.y;
    }
}