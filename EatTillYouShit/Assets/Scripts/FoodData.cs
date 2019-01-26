//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public enum FoodTypes
//{
//    // Drinks
//    milk,
//    wine,
//    greenSmoothie,
//    fruitSmoothie,
//    water,
//    lemonade,

//    // Starters
//    stuffedEggs,
//    salmonWraps,
//    toast,
//    scallop,
//    saladBowl,
//    soupBowl,
//    pickles,

//    // Main
//    stuffedPotatoes,
//    beans,
//    whitePasta,
//    tteokbokki,
//    chicken,
//    carrots,
//    porkBelly,
//    burgers,
//    corn,
//    sushi,
//    broccoli,
//    stuffedPaprika,

//    // Deserts
//    banana,
//    avocado,
//    papayaAndPeaches,
//    tiramisuIceCake,
//    iceCream,
//    apple,
//    chocolateMouse,
//}


//public enum MenuTypes
//{
//    drinks,
//    starters,
//    main,
//    desert
//}

//public struct FoodData
//{
//    public FoodData(FoodTypes type, int poopValue)
//    {
//        this.type = type;
//        this.poopValue = poopValue;
//    }

//    FoodTypes type;
//    int poopValue;
//}

//public class FoodDataManager
//{
//    static List<FoodData> drinks = new List<FoodData>()
//    {
//        new FoodData(FoodTypes.milk, 4),
//        new FoodData(FoodTypes.wine, 4),
//    };

//    static  List<FoodData> starters = new  List<FoodData>()
//    {
//        new FoodData(FoodTypes.stuffedEggs, 4),
//        new FoodData(FoodTypes.salmonWraps, 4),
//    };

//    static  List<FoodData> mains = new  List<FoodData>()
//    {
//        new FoodData(FoodTypes.stuffedPotatoes, 4),
//        new FoodData(FoodTypes.beans, 4),
//    };

//    static  List<FoodData> deserts = new  List<FoodData>()
//    {
//        new FoodData(FoodTypes.banana, 4),
//        new FoodData(FoodTypes.avocado, 4),
//    };

//    public static FoodData GetRandomFoodData(MenuTypes menuType)
//    {
//        var list = GetFoodList(menuType);

//        int r = Random.Range(0, list.Count - 1);

//        return list[r];
//    }

//    public static List<FoodData> GetFoodList(MenuTypes menuType)
//    {
//        switch(menuType)
//        {
//            default:
//            case MenuTypes.main:
//                return mains;
//            case MenuTypes.drinks:
//                return drinks;
//            case MenuTypes.starters:
//                return starters;
//            case MenuTypes.desert:
//                return deserts;
//        }
//    }
//}