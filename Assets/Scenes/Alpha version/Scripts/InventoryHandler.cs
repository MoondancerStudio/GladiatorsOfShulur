using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{

   // public ItemData newItemType;
    public int maxItems = 10;
    public List<ItemInstance> items = new();

    [SerializeField]
    public GameObject inventoryCells;

    [SerializeField]
    public GameObject equipCells;

    void Start()
    {
       // items.Add(new ItemInstance(newItemType));

    }

    public bool AddItem(ItemInstance itemToAdd)
    {
        // Finds an empty slot if there is one
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                return true;
            }
        }

        // Adds a new item if the inventory has space
        if (items.Count < maxItems)
        {
            items.Add(itemToAdd);
            return true;
        }

        Debug.Log("No space in the inventory");
        return false;
    }

    public void RemoveItem(ItemInstance itemToRemove)
    {
        items.Remove(itemToRemove);
    }
}
