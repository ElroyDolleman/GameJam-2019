using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListener : MonoBehaviour
{
    private int playerID;

    private AudioSource audioS;

    private void OnEnable()
    {
        EventManager.StartListening("SCORED_PLAYER" + playerID, PlaySound);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SCORED_PLAYER" + playerID, PlaySound);
    }

    private void Awake()
    {
        playerID = GetComponent<PlayerObject>().GetPlayerID();
        audioS = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        Debug.Log("Playing sound or something");
        audioS.Play();
    }
}
