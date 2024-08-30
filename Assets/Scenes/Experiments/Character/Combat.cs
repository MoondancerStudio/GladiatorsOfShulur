using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Combat : MonoBehaviour, ICombat
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
