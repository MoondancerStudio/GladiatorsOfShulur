using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private MyWeapon weapon;

    public MyWeapon GetWeapon()
    {
        return weapon; 
    }

    public void AddWeapon(MyWeapon weapon)
    {
        this.weapon = weapon; 
    }

    public void RemoveWeapon() { this.weapon = null;}
}
