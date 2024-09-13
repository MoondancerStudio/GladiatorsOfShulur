using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : ConfigurableCharacterBehaviour
{
    [SerializeField] private int attackBase;
    [SerializeField] private int defenseBase;

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
        // TODO: finding target
        // Calculate Attack Value
        int attackValue = attackBase + UnityEngine.Random.Range(1, 10);

        Debug.Log($"{name} attacks with [{attackValue}]");

        target.GetComponent<CharacterCombat>()?.Defend(attackValue, gameObject); // FIXME: Remove this part. Only for initial debug purposes.
    }

    public void Defend(int attackValue, GameObject attacker)
    {
        int defense = defenseBase + UnityEngine.Random.Range(1, 10);

        if (attackValue <= defense)
        {
            Debug.Log($"{name} parries");
        }
        else
        {
            Debug.Log($"{name} got hit");
            int rawDamage = -1 * Mathf.FloorToInt((attackValue - defense) / 2);

            this.GetComponent<CharacterHealth>().ChangeHealth(rawDamage);
        }
    }
}
