using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWeapon: MonoBehaviour, IWeapon
{
    [SerializeField] private int attackBonus;
    [SerializeField] private int defenseBonus;


    public MyWeapon() { }

    public MyWeapon(int attackBonus,int defenseBonus)
    {
        this.attackBonus = attackBonus;
        this.defenseBonus = defenseBonus;
    }

    public int AttackBonus()
    {
        return attackBonus;
    }

    public void AttackBonus(int value) { attackBonus = value; }

    public int DefenseBonus()
    {
        return defenseBonus;
    }

    public void DefenseBonus(int value) { defenseBonus = value; }
}
