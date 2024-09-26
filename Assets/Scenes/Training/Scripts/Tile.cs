using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private GameObject _hover;

    [SerializeField]
    public GameObject _highlight;


    public class OnPlayerMoveChangedArgs : EventArgs
    {
        public Vector2 position;
    }

    public event EventHandler<OnPlayerMoveChangedArgs> OnPlayerMoveChanged;

    public void Init(bool isWhite)
    {
        _renderer.color = isWhite ? new Color32(255, 206, 159, 255) : new Color32(255, 244, 195, 255);
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
            if (Unit.unitInstance != null)
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
}
