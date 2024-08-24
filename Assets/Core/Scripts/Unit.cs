using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public List<Vector2> possibleMoves;

    public void init(bool team, Vector2 pos)
    {
        if (team)
            color = Color.black;
        this.pos = pos;
        this.moves = new List<Vector2>();
        this.possibleMoves = new List<Vector2>();

        fillPossibleMoves();
    }

    public void fillPossibleMoves()
    {
      if(possibleMoves.Count > 0)
            this.possibleMoves.Clear();
        possibleMoves.Add(new Vector2(transform.position.x + 1, transform.position.y));
        possibleMoves.Add(new Vector2(transform.position.x - 1, transform.position.y));
        possibleMoves.Add(new Vector2(transform.position.x, transform.position.y + 1));
        possibleMoves.Add(new Vector2(transform.position.x, transform.position.y - 1));
    }

    public void updateMove()
    {
        possibleMoves.ForEach(possibleMove =>
        {
            Tile tile;
            if (GameObject.Find("GameHandler").GetComponent<GameHander>().tiles.TryGetValue(possibleMove, out tile))
            {
                if (tile.transform.tag == "tile")
                {
                    moves.Add(possibleMove);
                    Debug.Log("collect moves");
                }
            }
        });
    }

    public void Doattack(Unit unit)
    {
        if(unit != null)
        {
            unit.hp -= damage;
            GameObject.Find("enemy").transform.Find("Canvas (1)").transform.Find("Scrollbar").GetComponent<Scrollbar>().size -= unit.damage;
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

