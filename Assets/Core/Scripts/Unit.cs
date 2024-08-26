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
    float damage = 10.0f;
    float luck = 50.0f;

    public float hp = 100.0f;
    public List<Vector2> moves;
    public Vector2 pos;
    public List<Vector2> possibleMoves;
    public bool move;

    [SerializeField]
    public GameObject _hover;

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
            Vector3 newPos = new Vector2(possibleMove.x + transform.position.x, possibleMove.y + transform.position.y);
            Tile tile = GameObject.Find("GameHandler").GetComponent<GameHander>().getTile(newPos);

            if (tile != null)
            {
                if (tile.transform.tag.Equals("tile"))
                {
                    moves.Add(newPos);
                   // Debug.Log("collect moves");
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
            GameObject.Find("enemy").transform.Find("Canvas (1)").transform.Find("Scrollbar").GetComponent<Scrollbar>().size -= unit.damage * 0.01f;
          
            if(unit.hp <= 0)
            {
                Destroy(GameObject.Find("enemy"));
            }
        }
    }

    [Obsolete]
    void OnMouseDown()
    {
        if (moves.Count == 0 && !move && transform.name.Equals("player"))
        {
           updateMove();
        } else
        {
            Vector3 enemy = (Vector3)(GameObject.Find("enemy")?.gameObject.transform.position);

            if (enemy.x == transform.position.x && transform.position.y == enemy.y)
            {
                GameObject.Find("player").GetComponent<Unit>().Doattack(GameObject.Find("enemy").GetComponent<Unit>());
                GameObject.Find("enemy").GetComponent<ParticleSystem>().enableEmission = true;

                Tile t = GameObject.Find("GameHandler").GetComponent<GameHander>().getTile(transform.position);
                if (t != null)
                {
                    t.tilePossibleMovesDeactivate();
                    GameObject.Find("player").GetComponent<Unit>().move = true;
                }
            }
        }
    }

    void OnMouseEnter()
    {
        _hover.SetActive(true);
    }

    void OnMouseExit()
    {
        _hover.SetActive(false);
    }

    [Obsolete]
    void Update()
    {
        GameObject enemy = GameObject.Find("enemy");
        if (enemy != null && enemy!.GetComponent<ParticleSystem>().isEmitting)
        {
            if ((int)Time.time % 3 == 0)
            {
                enemy.GetComponent<ParticleSystem>().enableEmission = false;
            }
        }   

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


            // Have to check the two vectors angle, because of unpunctual value
            if (Vector2.Angle(transform.position, pos) < 0.1f)
            {
                transform.position = new Vector3((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y), -0.1f);
                move = false;
                moves.Clear();
                fillPossibleMoves();
            }
        }
    }
}

