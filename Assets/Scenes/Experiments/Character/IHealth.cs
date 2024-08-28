using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    int MaximumHitPoints();

    int ActualHitPoints();

    void SufferDamage(int amount);

    void Heal(int amount);

    void Die();
}
