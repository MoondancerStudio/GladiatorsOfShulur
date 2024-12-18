using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class AttackRespone : MonoBehaviour
{

    private Unit player;
    private DummyTrainerLogic enemy;
    private Animator animator;
    private Transform result;

    public class OnPlayerAttackChangedArgs : EventArgs
    {
        public string attackResult;
        public float damage;
        public Vector3 position;
    }

    void Start()
    {
        player = GameObject.Find("player").GetComponent<Unit>();
        enemy = GameObject.Find("enemy").GetComponent<DummyTrainerLogic>();
        if (player)
        {
            player.OnPlayerAttack += OnAtttack;
        }

        if (enemy)
        {
            enemy.OnEnemyAttack += OnAtttack;
        }
    }

    public static float calculateAttack(CharacterStat playerStat, CharacterStat enemyStat)
    {
        int d10_1 = UnityEngine.Random.Range(1, 10);
        int d10_2 = UnityEngine.Random.Range(1, 10);
        bool successAttack = (playerStat.baseAttack + d10_1) - (enemyStat.baseDefense + d10_2) > 0;
        if (successAttack)
        {
            return (playerStat.baseAttack + d10_1) - (enemyStat.baseDefense + d10_2) / 2;

        }
        return 0;
    }

    private void OnAtttack(object sender, AttackRespone.OnPlayerAttackChangedArgs e)
    {
        // animator = transform.Find(e.attackResult).GetComponent<Animator>();
        // animator.SetTrigger(e.attackResult);
        Debug.Log(e.attackResult.ToString());   
        result = transform.Find(e.attackResult);
        result.GetComponent<TextMeshProUGUI>().text = e.damage.ToString();
        result.gameObject.SetActive(true);
        Vector3 newPos = Camera.main.WorldToScreenPoint(e.position);
        result.position = new Vector3(newPos.x + 70.0f, newPos.y, newPos.z);
        player.isPlayerTurn = false;
       // Debug.Log(result.position.ToString());
    }


    void Update()
    {
        if (result != null) 
        {
            if ((int)Time.time % 4 != 0)
            {
                result.position += new Vector3(0.1f, 0.1f, 0);
            }
            else
            {
                result.gameObject.SetActive(false);
                result = null;
            }
        }
    }
}
