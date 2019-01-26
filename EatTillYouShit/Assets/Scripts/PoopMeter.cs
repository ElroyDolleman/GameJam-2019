using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopMeter : MonoBehaviour
{
    [SerializeField]
    Transform poopFillTransform;
    [SerializeField]
    float endFillPosY = -0.07f;

    float poopFillStartPosY = 0;

    // TODO: Maybe some other class should have this value, idk
    static int maxPoopValue = 32;

    float fromPoopValue = 0;
    float currentPoopValue = 0;
    float updateToPoopValue = 0;
    float easing = 0;

    float fillSpeed = 1;

    public bool isFull { get { return currentPoopValue == maxPoopValue; } }

    // Start is called before the first frame update
    void Start()
    {
        poopFillStartPosY = poopFillTransform.position.y;

        var pos = poopFillTransform.position;
        pos.y -= endFillPosY;
        poopFillTransform.position = pos;

        InvokeRepeating("TestPoopMeter", 1f, 1f);
    }

    void TestPoopMeter()
    {
        AddPoop(Random.Range(1, 5));
    }

    // Update is called once per frame
    void Update()
    {
        if (isFull) return;
        if (currentPoopValue == updateToPoopValue) return;
        if (updateToPoopValue > maxPoopValue) updateToPoopValue = maxPoopValue;

        currentPoopValue = Easing.EaseOutQuint(fromPoopValue, updateToPoopValue, easing += Time.deltaTime * fillSpeed);

        currentPoopValue = Mathf.Min(currentPoopValue, updateToPoopValue);

        float progress = 1 - currentPoopValue / maxPoopValue;

        var pos = poopFillTransform.position;
        pos.y = poopFillStartPosY * progress - endFillPosY;
        poopFillTransform.position = pos;

        if (isFull)
        {
            Debug.Log("It's time to take a shit!");
        }
    }

    public void AddPoop(int amountOfPoopToAdd)
    {
        updateToPoopValue += (float)amountOfPoopToAdd;
        fromPoopValue = currentPoopValue;
        easing = 0;
    }
}
