using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private List<Item> items;

    public void OnColllisionEnter2D()
    {
        // Get the GameObject that collided
        // If it has CharacterItemSlot Equip top item
        // If CharacterItemSlot has item, put it into items.
    }
}
