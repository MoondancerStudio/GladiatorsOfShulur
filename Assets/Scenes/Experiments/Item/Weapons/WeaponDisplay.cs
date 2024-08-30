using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDisplay : MonoBehaviour
{
    public ScriptableWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        this.name = weapon.name;
        this.GetComponentInParent<SpriteRenderer>().sprite = weapon.artwork;
        this.GetComponentInParent<MyWeapon>().AttackBonus(weapon.attackBonus);
        this.GetComponentInParent<MyWeapon>().DefenseBonus(weapon.defenseBonus);
    }
}
