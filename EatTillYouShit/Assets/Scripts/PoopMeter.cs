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
    float popupSpeed = 1;
    float scaleAwaySpeed = 2.5f;

    public bool isFull { get { return currentPoopValue == maxPoopValue; } }

    bool popupDone = false;
    bool scaleAway = false;

    // Start is called before the first frame update
    void Start()
    {
        poopFillStartPosY = poopFillTransform.localPosition.y;

        var pos = poopFillTransform.localPosition;
        pos.y -= endFillPosY;
        poopFillTransform.localPosition = pos;

        transform.localScale = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (scaleAway)
        {
            ScaleAwayUpdate();
            return;
        }

        if (currentPoopValue == updateToPoopValue) return;

        if (!popupDone)
        {
            ScaleInUpdate();
            return;
        }

        if (isFull) return;
        if (updateToPoopValue > maxPoopValue) updateToPoopValue = maxPoopValue;

        currentPoopValue = Easing.EaseOutQuint(fromPoopValue, updateToPoopValue, easing += Time.deltaTime * fillSpeed);

        currentPoopValue = Mathf.Min(currentPoopValue, updateToPoopValue);

        float progress = 1 - currentPoopValue / maxPoopValue;

        var pos = poopFillTransform.localPosition;
        pos.y = poopFillStartPosY * progress - endFillPosY;
        poopFillTransform.localPosition = pos;

        if (isFull)
        {
            Debug.Log("It's time to take a shit!");
            easing = 0;
        }
    }

    void StartScaleOut()
    {
        scaleAway = true;
        easing = 0;
    }

    void ScaleAwayUpdate()
    {
        easing = Mathf.Min(easing + Time.deltaTime * scaleAwaySpeed, 1);

        float scale = Easing.EaseInBack(0, 1, easing);

        transform.localScale = new Vector3(1 - scale, 1 - scale, 1);

        if (easing == 1)
        {
            easing = 0;
            scaleAway = false;
            popupDone = false;
        }
    }

    void ScaleInUpdate()
    {
        easing = Mathf.Min(easing + Time.deltaTime * popupSpeed, 1);

        float scale = Easing.EaseOutElastic(0, 1, easing);

        transform.localScale = new Vector3(scale, scale, 1);

        if (easing == 1)
        {
            easing = 0;
            popupDone = true;
            Invoke("StartScaleOut", 1);
        }
    }

    public void AddPoop(int amountOfPoopToAdd)
    {
        Debug.Log("Added Score" + amountOfPoopToAdd);
        updateToPoopValue += (float)amountOfPoopToAdd;

        if (updateToPoopValue > maxPoopValue)
            updateToPoopValue = maxPoopValue;

        fromPoopValue = currentPoopValue;
        easing = 0;
    }
}
