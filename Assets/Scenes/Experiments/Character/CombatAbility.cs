using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAbility : MonoBehaviour, ICombat
{
    [SerializeField] private int baseAttack;
    [SerializeField] private int baseDefense;

    public int BaseAttack()
    {
        return baseAttack;
    }

    public int BaseDefense()
    {
        return baseDefense;
    }
}
