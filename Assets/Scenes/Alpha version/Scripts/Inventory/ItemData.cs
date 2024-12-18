using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject, Weapon
{
    public string itemName;
    public Sprite icon;
    [TextArea]
    public string description;

    public int startingAmmo;
    public int startingCondition;

    public int attack;
    public int defense;

    public int getAttackBonus()
    {
        return attack;
    }

    public int getDefenseBonus()
    {
        return defense;
    }
}

