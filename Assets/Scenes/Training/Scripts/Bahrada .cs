using NUnit.Framework.Internal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Bahrada : Gladiator
{
    [SerializeField] private string characterName;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int[] hp;
    [SerializeField] int actualHp;
    [SerializeField] int speed;

    public override void Attack(int x, int y)
    {
        throw new NotImplementedException();
    }

    public override void HandleDamage(int damage)
    {
        throw new NotImplementedException();
    }

    public override void Move(int x, int y)
    {
        throw new NotImplementedException();
    }
}
