using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
public class ScriptableWeapon : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;

    public int attackBonus;
    public int defenseBonus;
}
