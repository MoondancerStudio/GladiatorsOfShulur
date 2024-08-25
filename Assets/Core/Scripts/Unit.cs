using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum UnitType
{
    Attacker,
    Defenser,
    Healer
}

public class Unit : MonoBehaviour
{
    float damage = 5.0f;
    float luck = 50.0f;

    public float hp = 100.0f;
    public List<Vector2> moves;
    public Vector2 pos;
    public List<Vector2> possibleMoves;
    public bool move;

    public void init(bool team, Vector2 pos)
    {
        this.pos = pos;
        this.moves = new List<Vector2>();
        this.possibleMoves = new List<Vector2>();
        this.move = false;

        fillPossibleMoves();
    }

    // Possible moves for default unit, could be vary for different type of unit
    public void fillPossibleMoves()
    {
        possibleMoves.Add(new Vector2(1,   0));
        possibleMoves.Add(new Vector2(-1,  0));
        possibleMoves.Add(new Vector2(0,   1));
        possibleMoves.Add(new Vector2(0, - 1));
    }

    public void updateMove()
    {
        possibleMoves.ForEach(possibleMove =>
        {
            Tile tile;

            Vector3 newPos = new Vector2(possibleMove.x + transform.position.x, possibleMove.y + transform.position.y);
            if (GameObject.Find("GameHandler").GetComponent<GameHander>().tiles.TryGetValue(newPos, out tile))
            {
                if (tile.transform.tag.Equals("tile"))
                {
                    moves.Add(newPos);
                    Debug.Log("collect moves");
                }
            }
        });
        GameObject.Find("GameHandler").GetComponent<GameHander>().isMoveHighlighted = false;
    }

    public void Doattack(Unit unit)
    {
        if(unit != null)
        {
            unit.hp -= damage;
            GameObject.Find("enemy").transform.Find("Canvas (1)").transform.Find("Scrollbar").GetComponent<Scrollbar>().size -= unit.damage * 0.1f;
        }
    }

    void OnMouseDown()
    {
        if (moves.Count == 0 && !move)
        {
           updateMove();
        }
    }

    void Update()
    {
        if (move)
        {
            if (pos.x > transform.position.x)
                transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, -0.5f);

            if (pos.x < transform.position.x)
                transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, -0.5f);

            if (pos.y > transform.position.y)
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, -0.5f);

            if (pos.y < transform.position.y)
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, -0.5f);
 
            if (Math.Abs(pos.y - transform.position.y) < 0.01 && Math.Abs(pos.x - transform.position.x) < 0.01)
            {
                transform.position = new Vector3((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y), -0.5f);
                move = false;
                moves.Clear();
                fillPossibleMoves();
            }
        }
    }
}

