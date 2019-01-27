using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour
{
    public float minSize;
    public float maxSize;
    public float multiplier;

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            //animation
            transform.localScale = new Vector3(minSize + Mathf.PingPong(Time.time * multiplier, maxSize), minSize + Mathf.PingPong(Time.time * multiplier, maxSize), transform.localScale.z);
        }
    }
}
