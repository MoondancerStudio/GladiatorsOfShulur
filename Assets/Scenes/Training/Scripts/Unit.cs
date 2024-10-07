﻿using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
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
    public float damage = 10.0f;
    public float hp = 100.0f;
    public bool move;
    public bool isPlayerTurn;

    public List<Vector2> moves;
    public Vector2 pos;
    public List<Vector2> possibleMoves;

    private float moveSpeed = 5f;
    private bool outOfStamina;
    private bool ishit;

    public static Unit unitInstance { get; private set; }

    void Awake()
    {
        if(transform.name.Equals("player"))
              unitInstance = this;
    }

    [SerializeField]
    public GameObject _hover;

    private Scrollbar _enemyHP;
    private Scrollbar _playerStamina;

    public Scrollbar PlayerStamina { get; private set; }

    void Start()
    {
        _enemyHP = GameObject.Find("enemy").transform.Find("UI/life").GetComponent<Scrollbar>();
        _playerStamina = GameObject.Find("player").transform.Find("UI/stamina").GetComponent<Scrollbar>();
        PlayerStamina = _playerStamina;
        ishit = false;
        isPlayerTurn = true;
    }

    public void init(bool team, Vector2 pos)
    {
        this.pos = pos;
        this.moves = new List<Vector2>();
        this.possibleMoves = new List<Vector2>();
        this.move = false;
        outOfStamina = false;
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
            Vector2 newPos = new Vector2(possibleMove.x + transform.position.x, possibleMove.y + transform.position.y);
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
            unit.hp -= damage * 0.1f;
            //    GameObject.Find("enemy").GetComponent<Animator>().SetBool("enemy_hurt", true);
            if (GameObject.Find("enemy").GetComponent<Animator>() != null)
                  GameObject.Find("enemy").GetComponent<Animator>().SetTrigger("hurt");

            if (GameObject.Find("enemy").GetComponent<DummyTrainerLogic>() != null)
                GameObject.Find("enemy").GetComponent<DummyTrainerLogic>().hitCount -= 1;

            if (GameObject.Find("Asd") != null)
                _enemyHP = GameObject.Find("enemy").transform.Find("UI/life").GetComponent<Scrollbar>();


            _enemyHP.size -= unit.damage * 0.01f;
           _playerStamina.size -= unit.damage * 0.05f;

            ColorBlock defaultColorBlock = _playerStamina.colors;
            defaultColorBlock.colorMultiplier += 0.3f * damage;
            _playerStamina.colors = defaultColorBlock;

            if (_playerStamina.size < 0.1)
            {
               // defaultColorBlock.normalColor = new Color(255,0,0);
               // _playerStamina.colors = defaultColorBlock;
                outOfStamina = true;
                _playerStamina.size += Time.deltaTime * 0.3f;
                defaultColorBlock = _playerStamina.colors;
                defaultColorBlock.colorMultiplier = 0;
                _playerStamina.colors = defaultColorBlock ;
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
        if (isPlayerTurn)
        {
            if (moves.Count == 0 && !move && transform.name.Equals("player"))
            {
                transform.Find("active").gameObject.SetActive(true);
                updateMove();
                GameObject.Find("Canvas").transform.Find("player_turn").GetComponent<TextMeshProUGUI>().faceColor = new Color32(0, 0, 0, 255);
                GameObject.Find("Canvas").transform.Find("enemy_turn").GetComponent<TextMeshProUGUI>().faceColor = new Color32(0, 255, 0, 255);
                isPlayerTurn = false;
            }
            else
            {
                Unit player = GameObject.Find("player").GetComponent<Unit>();

                if (player != null && player.moves.Contains(transform.position) && !player.outOfStamina)
                {
                    GameObject.Find("player").GetComponent<Unit>().Doattack(this);
                    player.transform.Find("active").GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                    //   GetComponent<ParticleSystem>().enableEmission = true;

                    Tile t = GameObject.Find("GameHandler").GetComponent<GameHander>().getTile(player.transform.position);

                    if (t != null)
                    {
                        t.tilePossibleMovesDeactivate();
                    }
                    player.move = true;
                    player.ishit = true;
                    GameObject.Find("Canvas").transform.Find("player_turn").GetComponent<TextMeshProUGUI>().faceColor = new Color32(0, 0, 0, 255);
                    GameObject.Find("Canvas").transform.Find("enemy_turn").GetComponent<TextMeshProUGUI>().faceColor = new Color32(0,255,0,255);
                    player.isPlayerTurn = !player.isPlayerTurn;
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
            _playerStamina.size += Time.deltaTime * 0.3f;
            ColorBlock defaultColorBlock = _playerStamina.colors;

            if (defaultColorBlock.colorMultiplier == 0 && (int)Time.time % 2 == 0)
            {
                defaultColorBlock.colorMultiplier = 1.01f;
            }

            if (defaultColorBlock.colorMultiplier > 1)
            {
                defaultColorBlock.colorMultiplier -= 0.01f;
                _playerStamina.colors = defaultColorBlock;
            }
        }

        if (_playerStamina.size == 1f && outOfStamina)
        {
          //  ColorBlock defaultColorBlock = _playerStamina.colors;
           // defaultColorBlock.normalColor = new Color(209, 111, 11);
          //  _playerStamina.colors = defaultColorBlock;
            outOfStamina = false;    
        }

        /*
        GameObject enemy = GameObject.Find("enemy");
        if (enemy != null && enemy!.GetComponent<ParticleSystem>().isEmitting)
        {
            if ((int)Time.time % 3 == 0)
            {
                enemy.GetComgoiponent<ParticleSystem>().enableEmission = false;
            }
        }   
        */

        if (ishit && (int)Time.time % 4 == 0)
        {
            ishit = !ishit;
            transform.Find("active").gameObject.SetActive(false);
            transform.Find("active").GetComponent<SpriteRenderer>().color = new Color32(42, 255, 0, 255);
        }

        if (move)
        {
            Vector3 direction = new Vector3(Mathf.Sign(pos.x - transform.position.x), Mathf.Sign(pos.y - transform.position.y), -0.5f).normalized;
         
            transform.Translate(new Vector3(direction.x * moveSpeed, direction.y * moveSpeed, direction.z) * Time.deltaTime);

            // Have to check the two vectors distance, because of unpunctual value
            if (Vector2.Distance(transform.position, pos) < 0.1f)
            {
                transform.position = new Vector3((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y), -0.1f);
                move = false;
                moves.Clear();
                if (transform.Find("active") != null && !ishit)
                    transform.Find("active").gameObject.SetActive(false);
            }
        }

        if (moves.Count > 0 || ishit)
        {
            transform.Find("active").Rotate(new Vector3((float)Math.Sin(0.5) * Time.timeScale, (float)Math.Cos(0.5) * Time.timeScale, 0));
        }
    }
}

