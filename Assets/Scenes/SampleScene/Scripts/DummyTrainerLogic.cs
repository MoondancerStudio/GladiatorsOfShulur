using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DummyTrainerLogic : MonoBehaviour
{
    private Unit unitComponent;

    void Start()
    {
        unitComponent = transform.GetComponent<Unit>();
        unitComponent.fillPossibleMoves();
    }

    void Update()
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
        } else if (Random.Range(-15, 15) == 1 && isPlayerAround())
        {
            // PlayerStamina.
            if (GameObject.Find("player").GetComponent<Unit>().hp > 0)
            {
                GameObject.Find("player").GetComponent<Unit>().hp -= unitComponent.damage * 0.02f;
                transform.GetComponent<Animator>().SetTrigger("attack");
            }
        }
    }

    bool isPlayerAround()
    {
        foreach (var possibleMove in unitComponent.possibleMoves)
        {
            Vector3 newPos = new Vector2(possibleMove.x + transform.position.x, possibleMove.y + transform.position.y);
            GameObject player = GameObject.Find("player");

            if (player.transform.position.x == newPos.x && player.transform.position.y == newPos.y)
            {
                 return true;
            }
        }
        return false;
    }
}
