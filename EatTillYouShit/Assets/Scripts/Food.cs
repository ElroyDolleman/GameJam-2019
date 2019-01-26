using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float poopValue;
    public GameObject outlineObject;

    [NonSerialized]
    public bool isTaken;

    [NonSerialized]
    public CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();

        if (circleCollider == null)
            Debug.LogWarning("Food needs a CircleCollider2D component");

        if (outlineObject == null)
            Debug.LogWarning("Food needs a CircleCollider2D component");
        else
            outlineObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateOutline()
    {
        outlineObject.SetActive(true);
    }

    public void DeactivateOutline()
    {
        outlineObject.SetActive(false);
    }
}
