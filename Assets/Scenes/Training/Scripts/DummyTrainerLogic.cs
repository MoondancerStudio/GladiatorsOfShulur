using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DummyTrainerLogic : MonoBehaviour
{
    private Unit unitComponent;

    [SerializeField]
    private Toggle ToggleTask;

    [SerializeField]
    private GameObject _enemyGo;

    public int hitCount;
    private int currentCount;

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
        if (currentCount == -2 && GameObject.Find("enemy") == null) {
            ToggleTask.isOn = true;
            ToggleTask.transform.Find("Task").GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            GameObject.Find("Canvas").transform.Find("Panel_next").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("Panel_next/msg").GetComponent<TextMeshProUGUI>().text = "Gratula, trainingnek vége!";
            currentCount = -3;
        }

        if (currentCount > hitCount && currentCount > 0)
        {
            currentCount = hitCount;
            ToggleTask.transform.Find("Task").GetComponent<TextMeshProUGUI>().text = $"Okozz {hitCount} sebzést a bábun!";
        }
        
        if(currentCount == 8)
        {
            currentCount = -2;
            ToggleTask.isOn = true;
            ToggleTask.transform.Find("Task").GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            GameObject.Find("Canvas").transform.Find("Panel_next").gameObject.SetActive(true);
        }

        if (Random.Range(-15, 15) % 3 == 0 && isPlayerAround() && (int)Time.time % 5 == 0)
        {
            if (GameObject.Find("player").GetComponent<Unit>().hp > 0)
            {
                float getBarValue = unitComponent.damage * 0.01f;
                GameObject.Find("player").GetComponent<Unit>().hp -= getBarValue;
                getBarValue /= 100;
                GameObject.Find("player").transform.Find("UI/life").GetComponent<Scrollbar>().size -= getBarValue;
                if (GameObject.Find("enemy").GetComponent<Animator>() != null)
                    transform.GetComponent<Animator>().SetTrigger("attack");
            }
        }

        if (Random.Range(-15, 15) % 4 == 0 && (int)Time.time % 5 == 0)
        {
            move();
        }
    }

    void move()
    {
        if (Random.Range(0, 6) == 1 && (int)Time.time % 10 == 0)
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
