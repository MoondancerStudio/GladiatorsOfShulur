
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class ItemInstance
{
    public ItemData itemType;
    public GameObject model;
    public int condition;
    public int ammo;

    public ItemInstance(ItemData itemData)
    {
        itemType = itemData;
       // condition = itemData.startingCondition;
       // ammo = itemData.startingAmmo;
    }
}