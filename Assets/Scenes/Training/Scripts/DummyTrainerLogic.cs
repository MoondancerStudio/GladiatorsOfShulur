using System;
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

    public int hitCount;
    private int currentCount;


    public event EventHandler<AttackRespone.OnPlayerAttackChangedArgs> OnEnemyAttack;

    void Start()
    {
        unitComponent = transform.GetComponent<Unit>();
        unitComponent.fillPossibleMoves();
        hitCount = 10;
        currentCount = 10;
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

        if (!GameObject.Find("player").GetComponent<Unit>().isPlayerTurn)
        {
            transform.Find("active").gameObject.SetActive(true);
            transform.Find("active").Rotate(new Vector3((float)Mathf.Sin(0.5f) * Time.timeScale, (float)Mathf.Cos(0.5f) * Time.timeScale, 0));

            if (Random.Range(-15, 15) % 3 == 0 && isPlayerAround() && (int)Time.time % 6 == 0)
            {
                if (GameObject.Find("player").GetComponent<Unit>().hp > 0)
                {
                    float damage = AttackRespone.calculateAttack(GetComponent<Unit>().stat, GameObject.Find("player").GetComponent<Unit>().stat);
                    if (damage > 0)
                    {
                        float getBarValue = damage * 0.3f;
                        GameObject.Find("player").GetComponent<Unit>().hp -= getBarValue;
                        getBarValue /= 100;
                        GameObject.Find("player").transform.Find("UI/life").GetComponent<Scrollbar>().size -= getBarValue;
                        if (GameObject.Find("enemy").GetComponent<Animator>() != null)
                            transform.GetComponent<Animator>().SetTrigger("attack");

                        OnEnemyAttack?.Invoke(this, new AttackRespone.OnPlayerAttackChangedArgs
                        {
                            attackResult = "Hit",
                            damage = unitComponent.damage_ * 0.8f,
                            position = transform.position
                        });
                    } else
                    {
                        OnEnemyAttack?.Invoke(this, new AttackRespone.OnPlayerAttackChangedArgs
                        {
                            attackResult = "Miss",
                        });
                     }
                }
                GameObject.Find("player").GetComponent<Unit>().isPlayerTurn = true;
                GameObject.Find("Canvas").transform.Find("player_turn").GetComponent<TextMeshProUGUI>().faceColor = new Color32(0, 255, 0, 255);
                GameObject.Find("Canvas").transform.Find("enemy_turn").GetComponent<TextMeshProUGUI>().faceColor = new Color32(0, 0, 0, 255);
                transform.Find("active").gameObject.SetActive(false);
                transform.Find("active").GetComponent<SpriteRenderer>().color = new Color32(42, 255, 0, 255);
            }

            if (Random.Range(-15, 15) % 4 == 0 && (int)Time.time % 5 == 0) //&& currentCount == -2)
            {
                move();
                GameObject.Find("player").GetComponent<Unit>().isPlayerTurn = true;
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
        GameObject enemy = GameObject.Find("enemy");
        if (enemy != null)
        {
            foreach (var possibleMove in unitComponent.possibleMoves)
            {
                Vector3 newPos = new Vector2(possibleMove.x + enemy.transform.position.x, possibleMove.y + enemy.transform.position.y);
                GameObject player = GameObject.Find("player");

                if (player.transform.position.x == newPos.x && player.transform.position.y == newPos.y)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
