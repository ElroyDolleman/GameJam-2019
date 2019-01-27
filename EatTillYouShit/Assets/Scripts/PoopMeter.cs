using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopMeter : MonoBehaviour
{
    public static List<PoopMeter> poopMeters;

    public static int nonPoopers
    {
        get
        {
            int count = 0;
            for (int i = 0; i < poopMeters.Count; i++)
            {
                if (!poopMeters[i].isFull)
                    count++;
            }

            return count;
        }
    }

    [SerializeField]
    Transform poopFillTransform;
    [SerializeField]
    SpriteRenderer fillRenderer;
    [SerializeField]
    float endFillPosY = -0.07f;

    Color fromFillColor = new Color(161f / 255f, 255f / 255f, 84f / 255f);
    Color toFillColor = new Color(138f / 255f, 24f / 255f, 22f / 255f);

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

    float shakePoint;
    public bool stayVisible = false;

    AudioSource audioSource;

    private void Awake()
    {
        if (poopMeters == null)
            poopMeters = new List<PoopMeter>();
        if (poopMeters.Count == 4)
            poopMeters.RemoveAt(0);

        poopMeters.Add(this);

        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        poopFillStartPosY = poopFillTransform.localPosition.y;

        var pos = poopFillTransform.localPosition;
        pos.y -= endFillPosY;
        poopFillTransform.localPosition = pos;

        transform.localScale = new Vector3(0, 0, 1);

        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (stayVisible) return;

        if (isFull)
        {
            // Shake that poop
            var p = transform.position;
            p.x = shakePoint + Random.Range(-0.1f, 0.1f);
            transform.position = p;

            return;
        }

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

        if (updateToPoopValue > maxPoopValue) updateToPoopValue = maxPoopValue;

        easing = Mathf.Min(easing + Time.deltaTime * fillSpeed, 1);
        currentPoopValue = Easing.EaseOutQuint(fromPoopValue, updateToPoopValue, easing);

        currentPoopValue = Mathf.Min(currentPoopValue, updateToPoopValue);

        float progress = 1 - currentPoopValue / maxPoopValue;

        UpdateColor();

        var pos = poopFillTransform.localPosition;
        pos.y = poopFillStartPosY * progress - endFillPosY;
        poopFillTransform.localPosition = pos;

        if (isFull)
        {
            Debug.Log("It's time to take a shit!");
            easing = 0;
            shakePoint = transform.position.x;
        }
    }

    void UpdateColor()
    {
        fillRenderer.color = Color.Lerp(fromFillColor, toFillColor, currentPoopValue / maxPoopValue);
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

        Invoke("PlayFartSound", 0.3f);
    }

    public void PlayFartSound()
    {
        audioSource.Play();
    }

    public float GetCurrentPoopValue()
    {
        return currentPoopValue;
    }
}
