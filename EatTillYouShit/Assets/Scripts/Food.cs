using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float poopValue;

    [NonSerialized]
    public CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();

        if (circleCollider == null)
            Debug.LogWarning("Food needs a CircleCollider2D component");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
