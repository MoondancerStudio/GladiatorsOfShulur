
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

class UIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _enemyTurn;

    [SerializeField]
    private TextMeshProUGUI _playerTurn;

    private bool _load = false;

     void Start()
    {
        _enemyTurn = GameObject.Find("Canvas/enemy_turn").GetComponent<TextMeshProUGUI>();
        _playerTurn = GameObject.Find("Canvas/player_turn").GetComponent<TextMeshProUGUI>();

    }

    private void LateUpdate()
    {
        if (!_load)
        {
            FindObjectsByType<Tile>(FindObjectsSortMode.None).ToList().ForEach(t => t.OnPlayerMoveChanged += UIController_OnPlayerMoveChanged);
           // FindFirstObjectByType<Tile>().OnPlayerMoveChanged += UIController_OnPlayerMoveChanged;
            _load = true;
        }
    }

    private void UIController_OnPlayerMoveChanged(object sender, Tile.OnPlayerMoveChangedArgs e)
    {
        _playerTurn.faceColor = new Color32(0, 0, 0, 255);
        _enemyTurn.faceColor = new Color32(0, 255, 0, 255);
    }
}

