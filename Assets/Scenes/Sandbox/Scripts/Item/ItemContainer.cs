using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private List<Item> items;

    void OnCollisionEnter2D(Collision2D col)
    {
        // Get the GameObject that collided
        // If it has CharacterItemSlot Equip top item
        // If CharacterItemSlot has item, put it into items.
        EquipFirst(col.gameObject);
    }

    private void EquipFirst(GameObject character)
    {
        Debug.Log("Try to equip first item");
        if (items != null && items.Count > 0)
        {
            Debug.Log("Equip first item");
            WeaponObject item = (WeaponObject)items[0];
            character.GetComponent<CharacterItemSlot>()?.Equip(item);
            items.RemoveAt(0);
        }
    }
}
