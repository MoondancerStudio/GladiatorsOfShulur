using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryHandler inventory;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out InstanceItemContainer foundItem))
        {
            if (!isInventoryFull(other))
            {
                Debug.Log($"Collect {foundItem}");
                inventory.AddItem(foundItem.TakeItem());
            } else
            {
                Debug.Log("Inventory is full!");
            }
        }
    }

    [System.Obsolete]
    private bool isInventoryFull(Collider2D other)
    {
        for (int i = 0; i < 12; i++)
        {
            Transform cell = inventory.inventoryCells.transform.GetChild(i);
            if (cell.GetChildCount() == 0)
            {
                other.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, -1.8f);
                other.transform.parent = cell;
                other.GetComponent<DragItem>().isDraggable = true;  
                return false;
            }
        }
        return true;
    }
}