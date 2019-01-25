using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[System.Serializable]
//public class Event : UnityEvent { }

public class EventManager : MonoBehaviour {

    //private Dictionary<string, Event> eventDictionary;
    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("No EventManager found in scene.");
                }
            }
            return eventManager;
        }
    }

    //public static void StartListening(string eventName, UnityAction listener){
    //	Event thisEvent = null;
    //	if ( instance.eventDictionary.TryGetValue ( eventName, out thisEvent ) ) {
    //		thisEvent.AddListener ( listener );
    //	} else {
    //		thisEvent = new Event();
    //		thisEvent.AddListener ( listener );
    //		instance.eventDictionary.Add (eventName, thisEvent);
    //	}
    //}

    //public static void StopListening(string eventName, UnityAction listener){
    //	if ( eventManager == null) {
    //		return;
    //	} 

    //	Event thisEvent = null;
    //	if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
    //		thisEvent.RemoveListener ( listener );
    //	}
    //}

    //public static void TriggerEvent(string eventName){
    //	Event thisEvent = null;
    //	if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
    //		thisEvent.Invoke ();
    //	}
    //}

    public delegate void OnFinish();
    public static event OnFinish onFinish;

    public delegate void OnGameStart();
    public static event OnGameStart onGameStart;

    public static void RaiseOnFinish(float seconds = 0f)
    {
        if (onFinish != null)
        {
            //instance.Invoke("onFinish", seconds);
            onFinish();
            //string name = onFinish.Method.Name;
            //instance.Invoke(name, seconds) ;
        }
    }

    public static void RaiseOnGameStart()
    {
        if (onGameStart != null)
        {
            onGameStart();
        }
    }
}
