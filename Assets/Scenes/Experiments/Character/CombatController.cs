using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public void Attack(Combat attacker, Combat target)
    {
        int attackValue = CalculateAttackValue(attacker);
        int defenseValue = CalculateDefenseValue(target);

        int damage = CalculateDamage(attackValue, defenseValue);

        DoDamage(damage, target);
    }

    private int CalculateAttackValue(Combat attacker)
    {
        int value = 0;
        int baseValue = this.GetComponentInParent<ICombat>().BaseAttack();

        List<IWeapon> weapons = new List<IWeapon>(attacker.GetComponentsInParent<IWeapon>());

        int valueBonuses = weapons.Select(weapon => weapon.AttackBonus()).Sum();

        value += baseValue + valueBonuses;

        return value;
    }

    private int CalculateDefenseValue(Combat target)
    {
        int value = 0;
        int baseValue = this.GetComponentInParent<ICombat>().BaseAttack();

        List<IWeapon> weapons = new List<IWeapon>(target.GetComponentsInParent<IWeapon>());

        int valueBonuses = weapons.Select(weapon => weapon.DefenseBonus()).Sum();

        value += baseValue + valueBonuses;

        return value;
    }

    private int CalculateDamage(int attackValue, int defenseValue)
    {
        float rawDamage = (attackValue + defenseValue) / 2;
        return Convert.ToInt32(Math.Floor(rawDamage));
    }

    private void DoDamage(int damage, Combat target)
    {
        List<IArmor> armors = new List<IArmor>(target.GetComponentsInParent<IArmor>());
        int reducedDamage = armors.Aggregate(damage, (damage, armor) => armor.ReduceDamage(damage));

        target.GetComponentInParent<IHealth>().SufferDamage(reducedDamage);
    }
}
