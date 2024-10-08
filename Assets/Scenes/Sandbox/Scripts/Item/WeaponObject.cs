using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item / Weapon")]
public class WeaponObject : Item
{
    public int attack;
    public int defense;

    public override void OnEquip(GameObject character)
    {
        character.GetComponent<CharacterCombat>()?.AlterAttack(attack);
        character.GetComponent<CharacterCombat>()?.AlterDefense(defense);
        Debug.Log($"{name} has been equiped");
    }

    public override void OnRemove(GameObject character)
    {
        character.GetComponent<CharacterCombat>()?.AlterAttack(-1 * attack);
        character.GetComponent<CharacterCombat>()?.AlterDefense(-1 * defense);
        Debug.Log($"{name} has been removed");
    }
}
