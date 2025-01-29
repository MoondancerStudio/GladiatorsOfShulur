using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    public GameObject inventoryCells;

    [SerializeField]
    public GameObject equipCells;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out InstanceItemContainer foundItem))
        {
            Transform cell = getEmptyCell();
            if (cell != null)
            {
                Debug.Log($"Stored {foundItem} in the inventory!");
             
                var getSpriteRenderer = other.GetComponent<SpriteRenderer>();
                if (other.GetComponent<Image>() == null)
                    other.AddComponent<Image>().sprite = getSpriteRenderer?.sprite;
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

    private Transform getEmptyCell()
    {
        return Enumerable.Range(0 ,12)
            .Select(x => inventoryCells.transform.GetChild(x))
            .FirstOrDefault(cell => cell.childCount == 0); 
    }
}