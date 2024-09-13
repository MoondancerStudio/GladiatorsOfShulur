using System;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent2Listener : MonoBehaviour
{
    public GameEvent2 gameEvent;
    public ParametrizedUnityEvent onEventTriggered = new ParametrizedUnityEvent();


    void OnEnable()
    {
        gameEvent.AddListener(this);
    }


    void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }

    public void OnEventTriggered(EventParameter parameter)
    {
        onEventTriggered.Invoke(parameter);
    }
}

[Serializable]
public class ParametrizedUnityEvent : UnityEvent<EventParameter> { }