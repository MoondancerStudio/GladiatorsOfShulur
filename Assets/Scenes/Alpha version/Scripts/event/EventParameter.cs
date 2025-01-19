using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event / Parameter")]
public class EventParameter : ScriptableObject
{
    private Dictionary<string, object> map = new Dictionary<string, object>();

    public object Get(string key)
    {
        return map[key];
    }

    public void Add(string key, object value)
    {
        map[key] = value;
    }
}