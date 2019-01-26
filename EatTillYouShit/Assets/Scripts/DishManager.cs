using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    public static DishManager instance;

    [SerializeField]
    FoodDish servingDish;
    [SerializeField]
    FoodDish waitingDish;

    [SerializeField, Range(0, 10)]
    float minAnticipationTime = 0.5f;

    [SerializeField, Range(0, 10)]
    float maxAnticipationTime = 4f;

    private void OnEnable()
    {
        EventManager.StartListening("EVERYONE_DONE", ServeNext);
    }

    private void OnDisable()
    {
        EventManager.StopListening("EVERYONE_DONE", ServeNext);
    }

    private void Awake()
    {
        if (instance != null)
            Debug.LogError("There should only be 1 instance of DishManager");
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // For testing, just for now, not for later
        //InvokeRepeating("ServeNext", 1, 3);
        ServeNext();
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

    public List<Food> GetCurrentFood()
    {
        return servingDish.GetAllFood();
    }

    public void RemoveFood(Food food)
    {
        servingDish.RemoveFood(food);
    }
}
