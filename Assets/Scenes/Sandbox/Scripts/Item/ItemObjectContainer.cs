using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectContainer : MonoBehaviour
{
    [SerializeField] GameObject itemObject;

    public void Take(GameObject itemObject)
    {
        this.itemObject = itemObject;
        this.itemObject.SetActive(false);
    }

    public void Drop()
    {
        Vector3 position = this.GetComponent<Transform>().position;
        itemObject.transform.position = position;
        itemObject.SetActive(true);
    }

    public void Equip(GameObject character)
    {
        this.itemObject.GetComponent<ConfigurableItem>()?.Equip(character);
    }
    public void Remove(GameObject character)
    {
        this.itemObject.GetComponent<ConfigurableItem>()?.Remove(character);
    }
}
