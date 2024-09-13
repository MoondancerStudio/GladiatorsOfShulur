using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event / One parameter")]
public class GameEvent2 : ScriptableObject
{
    private List<GameEvent2Listener> listeners = new List<GameEvent2Listener>();


    public void TriggerEvent(EventParameter parameter)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventTriggered(parameter);
        }
    }


    public void AddListener(GameEvent2Listener listener)
    {
        listeners.Add(listener);
    }


    public void RemoveListener(GameEvent2Listener listener)
    {
        listeners.Remove(listener);
    }
}