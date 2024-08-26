using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public abstract class Gladiator : MonoBehaviour
{
    private Armor armor;
    private Weapon weapon;

    public abstract void Move(int x, int y);
    public abstract void Attack(int x, int y);
    public abstract void HandleDamage(int damage);
}

