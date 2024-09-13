using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CharacterHealth : ConfigurableCharacterBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int actHealth;

    override protected void ConfigureValues()
    {
        maxHealth = config.maxHealth;
        actHealth = maxHealth;
    }

    public void ChangeHealth(int value)
    {
        actHealth += value;

        if (value < 0)
        {
            Damage(value);
        }
        else if (value > 0)
        {
            Heal(value);
        }
    }

    private void Damage(int amount)
    {
        Debug.Log($"{this.name}: Auch, that hurst! [{amount} HP]");

        if (actHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Trigger Character Died event
        Debug.Log($"{this.name} died.");
        GameObject.Destroy(this.gameObject);
    }

    private void Heal(int amount)
    {
        Debug.Log($"{this.name}: Ahh, that feels good! [+{amount} HP]");

        if (actHealth > maxHealth)
        {
            maxHealth = actHealth;
        }
    }
}
