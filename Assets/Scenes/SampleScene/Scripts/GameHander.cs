using System;
using System.Collections.Generic;
using UnityEngine;

public class GameHander : MonoBehaviour
{
    [SerializeField]
    private int _height, _width;

    [SerializeField]
    private Tile _tilepref;

    [SerializeField]
    private Tile _obstacle;

    [SerializeField]
    private Transform _cam;

    private Dictionary<Vector2, Tile> tiles;

    public Dictionary<Vector2, Tile> Tiles
    {
        get { return tiles; } 
        set { tiles = value; }  
    }

    public bool isMoveHighlighted = false;

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        tiles = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Tile spawned = Instantiate(_tilepref, new Vector3(x, y), Quaternion.identity);
                spawned.name = "Tile " + x + " " + y;
                spawned.tag = "tile";
                bool offset = (x + y) % 2 == 0;

                spawned.Init(offset);

                if (x == 2 && y == 4)
                {
                    //Place player unit
                    GameObject.Find("player").gameObject.transform.position = new Vector3(x, y, -0.1f);
                    GameObject.Find("player").GetComponent<Unit>().init(false, new Vector2(x, y));

                    //Place enemy unit
                    GameObject.Find("enemy").gameObject.transform.position = new Vector3(2, 2, -0.1f);
                  //  GameObject.Find("enemy").GetComponent<ParticleSystem>().enableEmission = false;
                }

                if(UnityEngine.Random.Range(0,4) == 1 && 
                    GameObject.Find("player").gameObject.transform.position.x != x && 
                    GameObject.Find("player").gameObject.transform.position.y != y &&
                    2 != x &&
                    2 != y)
                {
                    Tile obstacle = Instantiate(_obstacle, new Vector3(x, y, -0.1f), Quaternion.identity);
                    obstacle.name = "Obstacle " + x + " " + y;
                    spawned.tag = "obstacle";
                    obstacle.tag = "obstacle";
                }

                tiles.TryAdd(new Vector2(x, y), spawned);
            }
        }
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    public Tile getTile(Vector2 pos)
    {
        Tile tile;
        if (tiles.TryGetValue(pos, out tile))
        {
            return tile;
        }
        return null;
    }

    void highLightTiles(Tile tile, Vector2 pos)
    {
        tile._highlight.SetActive(true);
        isMoveHighlighted = true;
        tiles[pos] = tile;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoveHighlighted) 
        { 
            Unit player = GameObject.Find("player").GetComponent<Unit>();

            if (player != null)
            {
                if (player.moves.Count > 0)
                {
                    foreach (var m in player.moves)
                    {
                        Tile t = getTile(m);

                        if (t != null)
                            highLightTiles(t, m);         
                    }
                }
            }
        }
    }
}

