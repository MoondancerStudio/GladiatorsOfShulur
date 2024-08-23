using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

enum UnitType
{
    Attacker,
    Defense,
    Healer
}

public class Unit : MonoBehaviour
{
   public float hp = 100.0f;
    float damage = 5.0f;
    float luck = 50.0f;
    Color color = Color.white;
    public List<Vector2> moves;
    public Vector2 pos;

    public void init(bool team, Vector2 pos)
    {
        if (team)
            color = Color.black;
        this.pos = pos;
        this.moves = new List<Vector2>();
    }

    public void updateMove()
    {
        Debug.Log("collect moves");
        moves.Add(new Vector2(pos.x + 1, pos.y));
        moves.Add(new Vector2(pos.x - 1, pos.y));
        moves.Add(new Vector2(pos.x, pos.y + 1));
        moves.Add(new Vector2(pos.x, pos.y - 1));
    }

    public void Doattack(Unit unit)
    {
        if(unit != null)
        {
            unit.hp -= damage;
        }
    }

    void OnMouseDown()
    {
        if (moves.Count == 0)
        {
           updateMove();
        }
    }
}

