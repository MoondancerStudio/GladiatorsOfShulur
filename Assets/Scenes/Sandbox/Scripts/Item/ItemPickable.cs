using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ItemPickable : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision {collision.gameObject.tag}");
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<ConfigurableItem>()?.Equip(collision.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
