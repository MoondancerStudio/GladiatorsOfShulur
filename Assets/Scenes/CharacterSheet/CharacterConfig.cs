using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterConfig : ScriptableObject
{
    public new string name;
    public int maxHp;
    public int attachBase;
    public int defenseBase;

    public List<MyWeapon> weapons;

}
