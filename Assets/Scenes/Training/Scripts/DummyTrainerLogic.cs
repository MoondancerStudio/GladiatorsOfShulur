using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DummyTrainerLogic : MonoBehaviour
{
    private Unit unitComponent;

    [SerializeField]
    private Toggle ToggleTask;

    [SerializeField]
    private GameObject _enemyGo;

    private GameObject _playerGo;

    public int hitCount;
    private int currentCount;

    public event EventHandler<AttackResponse.OnPlayerAttackChangedArgs> OnEnemyAttack;

    void Start()
    {
        unitComponent = transform.GetComponent<Unit>();
        unitComponent.fillPossibleMoves();
        hitCount = 10;
        currentCount = 10;
        _playerGo = GameObject.Find("player");
    }

    public void click()
    {
        if (currentCount == -2)
        {
            GameObject.Find("Canvas").transform.Find("Panel_next").gameObject.SetActive(false);
            transform.name = "Asd";
            GameObject enemy = Instantiate(_enemyGo, new Vector3(transform.position.x, transform.position.y, -0.1f), Quaternion.identity);
            enemy.name = "enemy";
            transform.position = new Vector3(100, 100);

            ToggleTask.isOn = false;
            ToggleTask.transform.Find("Task").GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            ToggleTask.transform.Find("Task").GetComponent<TextMeshProUGUI>().text = $"Ölj meg valódi ellenfelet!";
            unitComponent = enemy.transform.GetComponent<Unit>();
            unitComponent.fillPossibleMoves();
            currentCount = -2;
        }
        else
        {
            GameObject.Find("Canvas").transform.Find("Panel_next").gameObject.SetActive(false);
        }
    }

    void Update()
    {

        if (currentCount > hitCount && currentCount > 0)
        {
         //   currentCount = hitCount;
          //  ToggleTask.transform.Find("Task").GetComponent<TextMeshProUGUI>().text = $"Okozz {hitCount} sebzést a bábun!";
        }
        
        if(currentCount == 0)
        {
            currentCount = -2;
            ToggleTask.isOn = true;
            ToggleTask.transform.Find("Task").GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            GameObject.Find("Canvas").transform.Find("Panel_next").gameObject.SetActive(true);
        }

        if (currentCount == -2 && GameObject.Find("enemy") == null) {
            ToggleTask.isOn = true;
            ToggleTask.transform.Find("Task").GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            GameObject.Find("Canvas").transform.Find("Panel_next").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("Panel_next/msg").GetComponent<TextMeshProUGUI>().text = "Gratula, trainingnek vége!";
            currentCount = -3;
        }

        if (!_playerGo.GetComponent<Unit>().isPlayerTurn)
        {
            transform.Find("active").gameObject.SetActive(true);
            transform.Find("active").Rotate(new Vector3((float)Mathf.Sin(0.5f) * Time.timeScale, (float)Mathf.Cos(0.5f) * Time.timeScale, 0));

            if (Random.Range(-15, 15) % 3 == 0 && isPlayerAround() && (int)Time.time % 6 == 0)
            {
                if (_playerGo.GetComponent<Unit>().hp > 0)
                {
                    Vector2 originPos = _enemyGo.transform.position;
                    while (Vector3.Distance(_enemyGo.transform.position, _playerGo.transform.position) > 0.1f)
                    {
                        _enemyGo.transform.position = Vector3.MoveTowards(
                            _enemyGo.transform.position,
                            _playerGo.transform.position,
                            0.001f * Time.deltaTime
                       );
                    }

                    while (Vector3.Distance(_enemyGo.transform.position, originPos) > 0.1f)
                    {
                        _enemyGo.transform.position = Vector3.MoveTowards(
                            _enemyGo.transform.position,
                            originPos,
                            0.001f * Time.deltaTime
                       );
                    }
                    transform.position = new Vector3((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y), -0.1f);
                    float damage = AttackResponse.calculateAttack(GetComponent<Unit>().stat, _playerGo.GetComponent<Unit>().stat);
                    if (damage > 0)
                    {
                        StartCoroutine(GameUtils.damageFlashingColor(_playerGo.GetComponent<SpriteRenderer>().color, _playerGo.GetComponent<SpriteRenderer>()));

                        float getBarValue = damage * 0.3f;
                        _playerGo.GetComponent<Unit>().hp -= getBarValue;
                        getBarValue /= 100;
                        GameObject.Find("Canvas/player_ui/Life").GetComponent<Scrollbar>().size -= getBarValue;
                        if (GameObject.Find("enemy").GetComponent<Animator>() != null)
                            transform.GetComponent<Animator>().SetTrigger("attack");

                        OnEnemyAttack?.Invoke(this, new AttackResponse.OnPlayerAttackChangedArgs
                        {
                            attackResult = "Hit",
                            damage = unitComponent.damage_ * 0.8f,
                            position = _playerGo.transform.position
                        });
                    }
                    else
                    {
                        OnEnemyAttack?.Invoke(this, new AttackResponse.OnPlayerAttackChangedArgs
                        {
                            attackResult = "Miss",
                            position = _playerGo.transform.position
                        });
                    }
                }
                _playerGo.GetComponent<Unit>().isPlayerTurn = true;
                GameObject.Find("Canvas").transform.Find("player_turn").GetComponent<TextMeshProUGUI>().faceColor = new Color32(0, 255, 0, 255);
                GameObject.Find("Canvas").transform.Find("enemy_turn").GetComponent<TextMeshProUGUI>().faceColor = new Color32(0, 0, 0, 255);
                transform.Find("active").gameObject.SetActive(false);
                transform.Find("active").GetComponent<SpriteRenderer>().color = new Color32(42, 255, 0, 255);
            }

            if (Random.Range(-10, 10) % 3 == 0 && (int)Time.time % 4 == 0)
            {
                move();

                _playerGo.GetComponent<Unit>().isPlayerTurn = true;
                GameObject.Find("Canvas").transform.Find("player_turn").GetComponent<TextMeshProUGUI>().faceColor = new Color32(0, 255, 0, 255);
                GameObject.Find("Canvas").transform.Find("enemy_turn").GetComponent<TextMeshProUGUI>().faceColor = new Color32(0, 0, 0, 255);
                transform.Find("active").gameObject.SetActive(false);
                transform.Find("active").GetComponent<SpriteRenderer>().color = new Color32(42, 255, 0, 255);
            }
        }
    }

    void move()
    {
        int random_x = (int)(Random.Range(-5, 5));
        int random_y = (int)(Random.Range(-5, 5));

        if (!(random_x >= 0 && random_x < 5))
        { 
            random_x = 0;
        }

        if (!(random_y >= 0 && random_y < 5))
        {
            random_y = 0;
        }

        unitComponent.pos = new Vector2(random_x, random_y);
          
        if (unitComponent.pos.x > 0 || unitComponent.pos.y > 0)
            unitComponent.move = true;
    }

    bool isPlayerAround()
    {
        if (_enemyGo != null && _playerGo != null)
        {
            Vector3 playerPos = _playerGo.transform.position;
            Vector3 enemyPos = _enemyGo.transform.position;
            return unitComponent.possibleMoves
                .Any(move => playerPos == new Vector3(move.x + enemyPos.x,
                                         move.y + enemyPos.y, playerPos.z));
      
        }
        return false;
    }
}
