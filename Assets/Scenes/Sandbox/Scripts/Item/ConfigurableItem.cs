using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ConfigurableItem: MonoBehaviour
{
    [SerializeField] Item item;

    private void Awake()
    {
        gameObject.name = item.name;
        GetComponent<SpriteRenderer>().sprite = item.icon;
    }

    public void Equip(GameObject character)
    {
        this.item?.OnEquip(character);
    }

    public void Remove(GameObject character)
    {
        this.item?.OnRemove(character);
    }
}
