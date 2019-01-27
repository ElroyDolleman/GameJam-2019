using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMiniTestje : MonoBehaviour
{
    public GameObject outline;

    private void Start()
    {
        outline.SetActive(false);
    }

    public void Activate()
    {
        outline.SetActive(true);
    }

    public void DeActivate()
    {
        outline.SetActive(false);
    }
}
