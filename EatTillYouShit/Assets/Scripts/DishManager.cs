using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    [SerializeField]
    FoodDish servingDish;
    [SerializeField]
    FoodDish waitingDish;

    [SerializeField, Range(0, 10)]
    float minAnticipationTime = 0.5f;

    [SerializeField, Range(0, 10)]
    float maxAnticipationTime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        // For testing, just for now, not for later
        InvokeRepeating("ServeNext", 1, 3);
    }

    void ServeNext()
    {
        SwapDishes();

        servingDish.ChangeState(FoodDish.DishStates.serve);
        waitingDish.ChangeState(FoodDish.DishStates.remove);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwapDishes()
    {
        var swap = servingDish;
        servingDish = waitingDish;
        waitingDish = swap;
    }
}
