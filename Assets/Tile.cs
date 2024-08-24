using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Color _black, _white;

    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    public GameObject _highlight;

    public Vector2 vec;

    public float delay = 20.0f;
    float timer = 0.0f;

    public void Init(bool isWhite)
    {
        _renderer.color = isWhite ? Color.blue : Color.white;
    }

    [System.Obsolete]
    void OnMouseDown()
    {
        if (_highlight.activeSelf)
        {
            Vector3 player = GameObject.Find("player").gameObject.transform.position;
            if (player != null)
            {
                Vector3 enemy = GameObject.Find("enemy").gameObject.transform.position;


                Debug.Log(player.x + " " + player.y);
                _highlight.SetActive(false);
               // animate(GameObject.Find("player").gameObject, vec);
                GameObject.Find("player").gameObject.transform.position = new Vector3(vec.x, vec.y, -0.5f);
                GameObject.Find("player").GetComponent<Unit>().pos = new Vector2(vec.x, vec.y);
                GameObject.Find("player").GetComponent<Unit>().moves.Clear();
                GameObject.Find("GameHandler").GetComponent<GameHander>().isMoveHighlighted = false;

                Vector3 newPlayerPos = GameObject.Find("player").gameObject.transform.position;
                if (enemy != null)
                {
                    if (enemy.x == newPlayerPos.x && newPlayerPos.y == enemy.y)
                    {
                        GameObject.Find("player").GetComponent<Unit>().Doattack(GameObject.Find("enemy").GetComponent<Unit>());

                        GameObject.Find("player").GetComponent<Unit>().pos = new Vector2(player.x, player.y);
                        GameObject.Find("player").gameObject.transform.position = new Vector3(player.x, player.y, -0.5f);
                        GameObject.Find("enemy").GetComponent<ParticleSystem>().enableEmission = true;
                    }
                }

                foreach (KeyValuePair<Vector2, Tile> item in GameObject.Find("GameHandler").GetComponent<GameHander>().tiles)
                {
                    Tile tile = item.Value.ConvertTo<Tile>();

                    if (tile._highlight.activeSelf)
                        tile._highlight.SetActive(false);
                }
            }
        }
    }

    void animate(GameObject p, Vector2 vec)
    {
        while (p.transform.position.x < vec.x)
        {
            p.transform.position = new Vector2(p.transform.position.x + 0.01f, p.transform.position.y);
        }

        while (p.transform.position.x > vec.x)
        {
            p.transform.position = new Vector2(p.transform.position.x - 0.01f, p.transform.position.y);
        }

        while (p.transform.position.y < vec.y)
        {
            p.transform.position = new Vector2(p.transform.position.x, p.transform.position.y + 0.01f);
        }

        while (p.transform.position.y > vec.y)
        {
            p.transform.position = new Vector2(p.transform.position.x, p.transform.position.y - 0.01f);
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
    }
}
