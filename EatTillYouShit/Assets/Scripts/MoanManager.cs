﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoanManager : MonoBehaviour
{
    public AudioClip[] clips;
    private PoopMeter poopMeter;
    private AudioSource audioS;

    private bool full = false;

    private void Awake()
    {
        poopMeter = GetComponentInParent<PlayerObject>().poopMeter;
        audioS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (poopMeter.GetCurrentPoopValue() >= 16 && poopMeter.GetCurrentPoopValue() <= 16.5f && !audioS.isPlaying)
        {
            audioS.PlayOneShot(clips[Random.Range(0, 2)]);
        } else if (poopMeter.isFull && !full)
        {
            full = true;
            audioS.PlayOneShot(clips[Random.Range(2, 4)]);
        }
    }
}
