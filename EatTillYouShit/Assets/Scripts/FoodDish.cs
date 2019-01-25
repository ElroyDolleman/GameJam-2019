using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDish : MonoBehaviour
{
    public enum DishStates
    {
        serve,
        anticipation,
        open,
        remove,
    }

    DishStates currentState;
    float easeStep = 0;
    float startPosY = 6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Serving()
    {
        // Move from top of screen to middle
        float progress = Easing.EaseOutQuint(1, 0, easeStep);
        float ypos = progress * startPosY;

        var pos = transform.position;
        pos.y = ypos;
        transform.position = pos;
    }

    public void ChangeState(DishStates newState)
    {
        currentState = newState;
    }
}
