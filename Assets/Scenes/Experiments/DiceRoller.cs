using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Singleton]
public class DiceRoller
{
    public int Roll(int sides = 10, int pieces = 1)
    {
        int value = 0;
        for (int i = 0; i < pieces; i++)
        {
            value += Random.Range(1, sides);
        }

        return value; 
    }
}
