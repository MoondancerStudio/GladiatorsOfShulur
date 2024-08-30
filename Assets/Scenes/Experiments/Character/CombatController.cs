using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    private DiceRoller dice;

    public void Start()
    {
        dice = new DiceRoller();
    }

    public void OnMouseDown()
    {
        Debug.Log("Click...");
        Combat attacker = this.GetComponentInParent<Combat>();
        Combat target = GameObject.FindWithTag("target")?.GetComponent<Combat>();

        if (target == null)
        {
            throw new Exception("No target found!");
        }
        Debug.Log(target.name);
        Attack(attacker, target);
    }

    public void Attack(Combat attacker, Combat target)
    {
        Debug.Log("Attack...");
        int attackValue = CalculateAttackValue(attacker);
        int defenseValue = CalculateDefenseValue(target);
        Debug.Log("Attack: " + attackValue + "; Defense: " + defenseValue);

        int damage = CalculateDamage(attackValue, defenseValue);
        Debug.Log("Damage " + damage);

        DoDamage(damage, target);
    }
    


    private int CalculateAttackValue(Combat attacker)
    {
        int value = 0;
        int baseValue = attacker.BaseAttack();

        List<WeaponSlot> weaponSlots = new List<WeaponSlot>(attacker.GetComponentsInParent<WeaponSlot>());

        int valueBonuses = weaponSlots.Select(slot => {
            MyWeapon weapon = slot.GetWeapon();
            if (weapon != null)
            {
                return weapon.AttackBonus();
            }
            return 0;
        }).Sum();

        value += baseValue + valueBonuses + dice.Roll();

        return value;
    }

    private int CalculateDefenseValue(Combat target)
    {
        int value = 0;
        int baseValue = target.BaseDefense();

        List<WeaponSlot> weaponSlots = new List<WeaponSlot>(target.GetComponentsInParent<WeaponSlot>());

        int valueBonuses = weaponSlots.Select(slot => {
            MyWeapon weapon = slot.GetWeapon();
            if (weapon != null)
            {
                return weapon.DefenseBonus();
            }
            return 0;
        }).Sum();

        value += baseValue + valueBonuses + dice.Roll();

        return value;
    }

    private int CalculateDamage(int attackValue, int defenseValue)
    {
        float rawDamage = (attackValue - defenseValue) / 2;
        return Convert.ToInt32(Math.Floor(rawDamage));
    }

    private void DoDamage(int damage, Combat target)
    {
        List<IArmor> armors = new List<IArmor>(target.GetComponentsInParent<IArmor>());
        int reducedDamage = armors.Aggregate(damage, (damage, armor) => armor.ReduceDamage(damage));

        target.GetComponentInParent<IHealth>().SufferDamage(reducedDamage);
    }
}
