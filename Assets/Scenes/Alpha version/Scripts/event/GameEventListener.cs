using System;
using UnityEngine;
using UnityEngine.Events;
 enum GameListenerTypes
{
    UI,
    GO,
    CONTROL
}

public class GameEventListener : MonoBehaviour
{
    // TODO: UI/GO/GAMECONTROL
    // move, attack, die, turn, spawn => action["move"] 
    // update method 
    private UnityEvent<EventParameter> onEventTriggered;
    private GameEvent _gameEvent;

    private GameListenerTypes _type;    
    
    void Awake()
    {
        _gameEvent = FindAnyObjectByType<GameEvent>();
    }

    void Start()
    {
        switch (tag)
        {
            case "player": 
                _type = GameListenerTypes.GO;
                break;
            case "UI":
                _type = GameListenerTypes.UI;
                break;
            case "game_control":
                _type = GameListenerTypes.CONTROL;
                break;
            default:
                break;
        }
    }

    void OnEnable()
    {
        _gameEvent.AddListener(this);
    }

    void OnDisable()
    {
        _gameEvent.RemoveListener(this);
    }

    public void OnEventTriggered(EventParameter eventParameter)
    {
        onEventTriggered.Invoke(eventParameter);
    }
}