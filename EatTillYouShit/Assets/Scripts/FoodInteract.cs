using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInteract : MonoBehaviour
{
    private List<Food> target = new List<Food>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //set sprite
        if (collision.GetComponent<Food>())
        {
            target.Add(collision.GetComponent<Food>());

            //activate the outline sprite
            //target[0].Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //remove sprite
        if (collision.GetComponent<Food>())
        {
            //deactivate the outline sprite
            //target[0].DeActivate();

            //Destroy(target[0].gameObject);

            target.RemoveAt(0);
        }
    }

    public Food GetTarget()
    {
        if (target.Count > 0)
            return target[0];
        else
            return null;
    }
}
