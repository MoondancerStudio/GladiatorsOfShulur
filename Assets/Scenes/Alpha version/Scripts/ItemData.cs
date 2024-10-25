using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea]
    public string description;
  //  public GameObject model;

    public int startingAmmo;
    public int startingCondition;
}

