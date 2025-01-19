using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(menuName = "Game Event / No parameter")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void TriggerEvents(EventParameter eventParameter)
    {
        Enumerable.Range(listeners.Count - 1, 0)
            .ToList()
            .ForEach(i =>
            {
                listeners[i].OnEventTriggered(eventParameter);
            });
    }

    public void AddListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}