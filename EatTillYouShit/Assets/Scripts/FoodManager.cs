using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField]
    List<Food> foodList;
    [SerializeField]
    List<Food> drinkList;

    List<Food> nextList;

    public static FoodManager instance;

    public bool shouldBruteForce { get { return drinkList.Count >= 4; } }

    // Start is called before the first frame update
    void OnEnable()
    {
        if (instance != null) Debug.LogError("There should only be one instance of FoodManager");
        instance = this;

        nextList = foodList;

        Debug.Log(foodList.Count);
    }

    public Food GetRandomFood(ref int index)
    {
        index = Random.Range(0, nextList.Count);
        return nextList[index];
    }

    public Food GetFood(int index)
    {
        return nextList[index];
    }

    public void ChooseNextRandomMenu()
    {
        nextList = Random.Range(0f, 1f) > 0.5f ? foodList : drinkList;
    }
}
