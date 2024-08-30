using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private int maximumHitPoints;
    [SerializeField] private int actualHitPoints;

    public int MaximumHitPoints()
    {
        return maximumHitPoints;
    }

    public int ActualHitPoints()
    {
        return actualHitPoints;
    }

    public void SufferDamage(int damage)
    {
        if (damage > 0)
        {
            actualHitPoints -= damage;
            Debug.Log(this.name + ": Auch! That's hurt.");

            if (actualHitPoints <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        Debug.Log(this.name + ": I'm dead.");
        Destroy(gameObject);
    }

    public void Heal(int amount)
    {
        if (actualHitPoints + amount > maximumHitPoints)
        {
            actualHitPoints = maximumHitPoints;
            Debug.Log("I'm on full health.");
        } else
        {
            actualHitPoints += amount;
            Debug.Log("I healed.");
        }
    }
}