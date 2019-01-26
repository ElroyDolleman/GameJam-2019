using System.Collections;
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

    BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
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
        for (int i = 0; i < 4; i++)
        {
            SpawnRandomFood();
        }
    }

    bool CheckFoodOverlap(Food food)
    {
        Vector2 foodPos = (Vector2)food.transform.position + food.circleCollider.offset;

        for (int i = 0; i < foodList.Count; i++)
        {
            Vector2 otherPos = (Vector2)foodList[i].transform.position + foodList[i].circleCollider.offset;

            float dist = Vector2.Distance(otherPos, foodPos);
            float radiusSum = foodList[i].circleCollider.radius + food.circleCollider.radius;

            if (radiusSum > dist)
                return true;
        }

        return false;
    }

    void SpawnRandomFood()
    {
        Food foodPrefab = FoodManager.instance.GetRandomFood(MenuTypes.starters);

        Food newFood = GameObject.Instantiate(foodPrefab);
        newFood.transform.SetParent(transform);

        // Brute force a location that doesn't overlap another food 
        int safe = 0;
        while(true)
        {
            PlaceFoodRandomly(newFood);

            if (!CheckFoodOverlap(newFood))
            {
                break;
            }
            else if (safe++ > 1000)
            {
                Debug.Log("Failed to place food at a correct place");
                break;
            }
        }

        foodList.Add(newFood);
    }

    void PlaceFoodRandomly(Food food)
    {
        float left = boxCollider.offset.x - boxCollider.size.x / 2;
        float right = boxCollider.offset.x + boxCollider.size.x / 2;
        float bottom = boxCollider.offset.y - boxCollider.size.y / 2;
        float top = boxCollider.offset.y + boxCollider.size.y / 2;

        float minX = left + food.circleCollider.radius;
        float maxX = right - food.circleCollider.radius;
        float minY = bottom + food.circleCollider.radius;
        float maxY = top - food.circleCollider.radius;

        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);

        food.transform.localPosition = new Vector3(randX, randY, 0);
    }

    //void PlaceFoodRandomly(Food food)
    //{
    //    float offX = Mathf.Abs(food.circleCollider.offset.x);
    //    float offY = Mathf.Abs(food.circleCollider.offset.y);
    //    float extraOffset = Mathf.Max(offX, offY);
    //    float maxRadius = circleCollider.radius - food.circleCollider.radius - extraOffset;

    //    float randomRadius = Random.Range(0, maxRadius);
    //    float randomAngle = Random.Range(0, Mathf.Deg2Rad * 360);

    //    float rangeX = randomRadius * Mathf.Cos(randomAngle);
    //    float rangeY = randomRadius * Mathf.Sin(randomAngle);

    //    food.transform.localPosition = new Vector3(rangeX, rangeY, 0);
    //}

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