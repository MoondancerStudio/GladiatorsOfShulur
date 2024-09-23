using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : ConfigurableCharacterBehaviour
{
    [SerializeField] private int attackBase;
    [SerializeField] private int defenseBase;

    public void AlterAttack(int value)
    {
        attackBase += value;
    }

    public void AlterDefense(int value)
    {
        defenseBase += value;
    }

    override protected void ConfigureValues()
    {
        attackBase = config.attackBase;
        defenseBase = config.defenseBase;
    }

    private void Test()
    {
        Attack(this.gameObject);
    }

    public void Attack(GameObject target)
    {
        // Calculate Attack Value
        int attackValue = attackBase + UnityEngine.Random.Range(1, 10);

        Debug.Log($"{name} attacks {target.name} with [{attackValue}]");

        target.GetComponent<CharacterCombat>()?.Defend(attackValue, gameObject); // FIXME: Refactor it to event based solution.
        // attacked.Invoke(target, attackValue, attacker); // Is this ideal?
    }

    public void Defend(int attackValue, GameObject attacker)
    {
        int defense = defenseBase + UnityEngine.Random.Range(1, 10);

        if (attackValue <= defense)
        {
            Debug.Log($"{name} parries [{attackValue} vs {defense}]");
        }
        else
        {
            Debug.Log($"{name} got hit [{attackValue} vs {defense}]");
            int rawDamage = -1 * Mathf.FloorToInt((attackValue - defense) / 2);

            this.GetComponent<CharacterHealth>().ChangeHealth(rawDamage);
        }
    }

    public void OnAttackControl(EventParameter parameter)
    {
        if (parameter != null && parameter.Get("attackVector") != null && parameter.Get("attackVector") is Vector3 attackVector)
        {
            Debug.Log($"{name}: Searching for target with attack vector [{attackVector.x}, {attackVector.y}]");

            // TODO: Finding the target
            var target = GameObject.FindWithTag("target");

            Attack(target);
        }
    }
}
