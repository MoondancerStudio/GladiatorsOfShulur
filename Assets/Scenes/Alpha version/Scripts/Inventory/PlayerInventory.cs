using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//Vector3(365,329,-998)


public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    public GameObject inventoryCells;

    [SerializeField]
    public GameObject equipCells;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out InstanceItemContainer foundItem))
        {
            Transform cell = getEmptyCell();
            if (cell != null)
            {
                Debug.Log($"Stored {foundItem} in the inventory!");
              //  inventory.AddItem(foundItem.TakeItem());

                other.AddComponent<Image>().sprite = other.GetComponent<SpriteRenderer>().sprite;
                other.GetComponent<DragItem>().isDraggable = true;  
             
                other.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, 0);
                other.transform.SetParent(cell);
                other.transform.localScale = Vector3.one;
            } else
            {
                Debug.Log("Inventory is full!");
            }
        }
    }

    [System.Obsolete]
    private Transform getEmptyCell()
    {
        for (int i = 0; i < 12; i++)
        {
            Transform cell = inventoryCells.transform.GetChild(i);

            if (cell.GetChildCount() == 0)
                return cell;
            
        }
        return null;
    }
}