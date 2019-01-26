using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuTypes
{
    drinks,
    starters,
    main,
    desert
}

public class FoodManager : MonoBehaviour
{
    [SerializeField]
    List<Food> drinks;
    [SerializeField]
    List<Food> starters;
    [SerializeField]
    List<Food> mains;
    [SerializeField]
    List<Food> deserts;

    public static FoodManager instance;

    public bool shouldBruteForce { get { return starters.Count >= 4; } }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null) Debug.LogError("There should only be one instance of FoodManager");
        instance = this;
    }

    public Food GetRandomFood(MenuTypes menuType, ref int index)
    {
        var list = GetFoodList(menuType);

        index = Random.Range(0, list.Count);

        return list[index];
    }

    public List<Food> GetFoodList(MenuTypes menuType)
    {
        switch (menuType)
        {
            default:
            case MenuTypes.main:
                return mains;
            case MenuTypes.drinks:
                return drinks;
            case MenuTypes.starters:
                return starters;
            case MenuTypes.desert:
                return deserts;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
