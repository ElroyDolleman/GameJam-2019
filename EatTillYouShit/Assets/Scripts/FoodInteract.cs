using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInteract : MonoBehaviour
{
    private List<FoodMiniTestje> target = new List<FoodMiniTestje>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //set sprite
        if (collision.GetComponent<FoodMiniTestje>())
        {
            target.Add(collision.GetComponent<FoodMiniTestje>());
            target[0].Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //remove sprite
        if (collision.GetComponent<FoodMiniTestje>())
        {
            target[0].DeActivate();
            //Destroy(target[0].gameObject);
            target.RemoveAt(0);
        }
    }

    public FoodMiniTestje GetTarget()
    {
        if (target.Count > 0)
            return target[0];
        else
            return null;
    }
}
