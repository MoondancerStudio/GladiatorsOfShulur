﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
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
    //float luck = 50.0f;
    float moveSpeed = 5f;

    public float hp = 100.0f;
    public List<Vector2> moves;
    public Vector2 pos;
    public List<Vector2> possibleMoves;
    public bool move;

    private bool outOfStamina;
    private bool isPlayerTurn;


    public static Unit unitInstance { get; private set; }

    void Awake()
    {
        if(transform.name.Equals("player"))
              unitInstance = this;
    }

    [SerializeField]
    public GameObject _hover;

    public void init(bool team, Vector2 pos)
    {
        this.pos = pos;
        this.moves = new List<Vector2>();
        this.possibleMoves = new List<Vector2>();
        this.move = false;
        outOfStamina = false;
        isPlayerTurn = true;
        fillPossibleMoves();
        
    }

    public void Unit_playerMoveEvent(object sender, Tile.OnPlayerMoveChangedArgs e)
    {
        pos = new Vector2(e.position.x, e.position.y);
        move = true;
    }

    // Possible moves for default unit, could be vary for different type of unit
    public void fillPossibleMoves()
    {
        possibleMoves.Add(new Vector2( 1,   0));
        possibleMoves.Add(new Vector2(-1,   0));
        possibleMoves.Add(new Vector2( 0,   1));
        possibleMoves.Add(new Vector2( 0, - 1));
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
            GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().size -= unit.damage * 0.05f;

            ColorBlock defaultColorBlock = GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().colors;
            defaultColorBlock.colorMultiplier += 0.3f * damage;
            GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().colors = defaultColorBlock;

            if (GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().size < 0.1)
            {
               // defaultColorBlock.normalColor = new Color(255,0,0);
               // GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().colors = defaultColorBlock;
                outOfStamina = true;
                GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().size += Time.deltaTime * 0.3f;
                defaultColorBlock = GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().colors;
                defaultColorBlock.colorMultiplier = 0;
                GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().colors = defaultColorBlock ;
            }

            if (unit.hp <= 0)
            {
                Tile tile = GameObject.Find("GameHandler").GetComponent<GameHander>().getTile(GameObject.Find("enemy").transform.position);
                if (tile != null) 
                {
                    move = true;
                    Destroy(GameObject.Find("enemy"));
                }
            }
        }
    }

    [Obsolete]
    void OnMouseDown()
    {

        if (moves.Count == 0 && !move && transform.name.Equals("player"))
        {
           updateMove();
        } 
        else
        {
            Unit player = GameObject.Find("player").GetComponent<Unit>();

            if (GameObject.Find("enemy") != null && moves.Contains(GameObject.Find("enemy").transform.position))
            {
                player.move = true;
            }

            if (player != null && player.moves.Contains(transform.position) && !GameObject.Find("player").GetComponent<Unit>().outOfStamina)
            {
                GameObject.Find("player").GetComponent<Unit>().Doattack(this);
                GetComponent<ParticleSystem>().enableEmission = true;

                Tile t = GameObject.Find("GameHandler").GetComponent<GameHander>().getTile(player.transform.position);

                if (t != null)
                {
                    t.tilePossibleMovesDeactivate();
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

        if (outOfStamina)
        {
            GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().size += Time.deltaTime * 0.3f;
            ColorBlock defaultColorBlock = GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().colors;
          //  Debug.Log(Time.time);
           // Debug.Log((int)Time.time);
            if (defaultColorBlock.colorMultiplier == 0 && (int)Time.time % 2 == 0)
            {
                defaultColorBlock.colorMultiplier = 1.01f;
            }

            if (defaultColorBlock.colorMultiplier > 1)
            {
                defaultColorBlock.colorMultiplier -= 0.01f;
                GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().colors = defaultColorBlock;
            }
        }

        if (GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().size == 1f && outOfStamina)
        {
          //  ColorBlock defaultColorBlock = GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().colors;
           // defaultColorBlock.normalColor = new Color(209, 111, 11);
          //  GameObject.Find("player").transform.Find("Canvas").transform.Find("Scrollbar").GetComponent<Scrollbar>().colors = defaultColorBlock;
            outOfStamina = false;    
        }

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
                transform.position += new Vector3(moveSpeed, 0, -0.5f) * Time.deltaTime;

            if (pos.x < transform.position.x)
                transform.position += new Vector3(-moveSpeed, 0, -0.5f) * Time.deltaTime;

            if (pos.y > transform.position.y)
                transform.position += new Vector3(0, moveSpeed, -0.5f) * Time.deltaTime;

            if (pos.y < transform.position.y)
                transform.position += new Vector3(0, -moveSpeed, -0.5f) * Time.deltaTime;

            // Have to check the two vectors distance, because of unpunctual value
            if (Vector2.Distance(transform.position, pos) < 0.1f)
            {
                transform.position = new Vector3((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y), -0.1f);
                move = false;
                moves.Clear();
                fillPossibleMoves();
            }
        }
    }
}
