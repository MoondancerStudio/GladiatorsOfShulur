using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{

    public new string name;
    public Sprite icon;

    public abstract void OnEquip(GameObject character);
    public abstract void OnRemove(GameObject character);
}
