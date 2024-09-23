using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItemSlot : MonoBehaviour
{
    [SerializeField] private WeaponObject hand;

    public void OnEnable()
    {
        hand?.OnEquip(this.gameObject);
    }

    public void OnDisable()
    {
        hand?.OnRemove(this.gameObject);
    }

    public void Equip(WeaponObject weapon)
    {
        if (hand != null)
        {
            Remove();
        }

        hand = weapon;
        hand.OnEquip(this.gameObject);
    }

    public void Remove()
    {
        hand?.OnRemove(this.gameObject);
        hand = null;
    }

    public void OnItemEquip(EventParameter parameter)
    {
        if (parameter != null && parameter.Get("item") != null)
        {
            WeaponObject item = (WeaponObject)parameter.Get("item");
            Equip(item);
        }
    }
}
