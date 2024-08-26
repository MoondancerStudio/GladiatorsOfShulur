using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    public GameObject _highlight;

    [SerializeField]
    public GameObject _hover;

    public class OnPlayerMoveChangedArgs : EventArgs
    {
        public Vector2 position;
    }

    public event EventHandler<OnPlayerMoveChangedArgs> OnPlayerMoveChanged;

    public void Init(bool isWhite)
    {
        _renderer.color = isWhite ? Color.blue : Color.white;

        OnPlayerMoveChanged += Unit.unitInstance.Unit_playerMoveEvent;
    }

    void OnMouseEnter()
    {
        _hover.SetActive(true);
    }

    void OnMouseExit()
    {
        _hover.SetActive(false);
    }

    [System.Obsolete]
    void OnMouseDown()
    {
        if (_highlight.activeSelf)
        {
            GameObject player = GameObject.Find("player");
            if (player != null)
            {
                OnPlayerMoveChanged?.Invoke(this, new OnPlayerMoveChangedArgs
                {
                    position = new Vector2(transform.position.x, transform.position.y)
                });

                tilePossibleMovesDeactivate();
            }
        }
    }

    public void tilePossibleMovesDeactivate()
    {
        foreach (KeyValuePair<Vector2, Tile> item in GameObject.Find("GameHandler").GetComponent<GameHander>().Tiles)
        {
            Tile tile = item.Value.ConvertTo<Tile>();

            if (tile._highlight.activeSelf)
                tile._highlight.SetActive(false);
        }
    }

    void Update()
    {
    }
}
