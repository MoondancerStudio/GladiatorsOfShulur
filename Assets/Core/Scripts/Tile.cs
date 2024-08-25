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

    public void Init(bool isWhite)
    {
        _renderer.color = isWhite ? Color.blue : Color.white;
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
                Vector3 enemy = GameObject.Find("enemy").gameObject.transform.position;
                if (enemy != null)
                {
                    
                    if (enemy.x == transform.position.x && transform.position.y == enemy.y)
                    {
                        GameObject.Find("player").GetComponent<Unit>().Doattack(GameObject.Find("enemy").GetComponent<Unit>());
                        GameObject.Find("enemy").GetComponent<ParticleSystem>().enableEmission = true;
                    }
                    else
                    {
                        GameObject.Find("player").GetComponent<Unit>().pos = new Vector2(transform.position.x, transform.position.y);
                    }
                }
                GameObject.Find("player").GetComponent<Unit>().move = true;
                tilePossibleMovesDeactivate();
            }
        }
    }

    void tilePossibleMovesDeactivate()
    {
        foreach (KeyValuePair<Vector2, Tile> item in GameObject.Find("GameHandler").GetComponent<GameHander>().tiles)
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
