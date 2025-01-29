using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


public class AttackResponse : MonoBehaviour
{

    private Unit player;
    private DummyTrainerLogic enemy;
    private Animator animator;
    private Transform _result;

    private IAsyncEnumerator<Action> asyncEnumerator;

    private List<Transform> hitResult; 

    public class OnPlayerAttackChangedArgs : EventArgs
    {
        public string attackResult;
        public float damage;
        public Vector3 position;
        public Vector3 source_position;
    }

    void Start()
    {
        player = GameObject.Find("player").GetComponent<Unit>();
        enemy = GameObject.Find("enemy").GetComponent<DummyTrainerLogic>();

        player.OnPlayerAttack += OnAtttack;
        enemy.OnEnemyAttack += OnAtttack;
    }

    public async Task HandleAllHitResultsAsync()
    {
        print("start");
        var hitResults = GameObject.FindGameObjectsWithTag("result")
                              .Select(obj => obj.transform)
                              .AsParallel();

        var tasks = new List<Task>();

        foreach (var hit in hitResults)
        {
            tasks.Add(HandleHitResultAsync(hit));
        }

        await Task.WhenAll(tasks);
        print("Finish");
    }

    private async Task HandleHitResultAsync(Transform hitResult)
    {
        await Task.Run(async () =>
        {
            while (hitResult.position.x <= 450.0f)
            {
                await Task.Delay(100);
               // moveHitResult(hitResult);
            }
        });
    }

    IEnumerator hitTextFloatingAway(Transform hitResult)
    {
      //  print("start");

         yield return new WaitWhile(() =>
        {
            //  print("process" + " " + hitResult.position.x);
            if (hitResult != null)
            {
                if (hitResult.position.x <= 450.0f)
                {
                    return true;
                }
                else
                {
                    Destroy(hitResult.gameObject);
                    return false;
                }
            }
            return false;
        });
      //  print("Finish");
    }

    IEnumerator runFloatingAwayCoroutine()
    {
        var hitResult = GameObject.FindGameObjectsWithTag("result")?
                      .Select(obj => obj.transform)
                      .ToList();

        while (GameObject.FindGameObjectsWithTag("result") != null) 
        { 
            foreach (var t in hitResult)
            {
                yield return hitTextFloatingAway(t);
            }
        }
        CancelInvoke("moveHitResult");
    }

    // Coroutine, amely elindítja az async metódust
    public IEnumerator RunAsyncInCoroutine()
    {
        Task asyncTask = HandleAllHitResultsAsync();

        // Várunk, amíg az async feladat befejezõdik
        while (!asyncTask.IsCompleted)
        {
            yield return null; // Várakozás a következõ frame-ig
        }

        // Ha hiba történt az async futás során
        if (asyncTask.IsFaulted)
        {
            Debug.LogError(asyncTask.Exception);
        }
    }

    void moveHitResult()
    {
     GameObject.FindGameObjectsWithTag("result")?
                .Select(obj => obj.transform)
                .ToList()
                .ForEach(t => t.position += new Vector3(0.5f, 0.5f, 0));
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

    private void OnAtttack(object sender, OnPlayerAttackChangedArgs e)
    {
        // animator = transform.Find(e.attackResult).GetComponent<Animator>();
        // animator.SetTrigger(e.attackResult);
      //  Vector2 direction = (e.position - e.source_position).normalized;
        //   transform.Translate(new Vector3(direction.x * moveSpeed, direction.y * moveSpeed) * Time.deltaTime);

        Debug.Log(e.attackResult.ToString());   
        Vector3 newPos = Camera.main.WorldToScreenPoint(e.position);
        Transform result = Instantiate(transform.Find(e.attackResult), new Vector3(newPos.x + 70.0f, newPos.y, newPos.z), Quaternion.identity, transform.parent);
        result.GetComponent<TextMeshProUGUI>().text = e.damage.ToString();
        result.gameObject.SetActive(true);
       
        player.isPlayerTurn = false;
        // Debug.Log(result.position.ToString());
        StartCoroutine(runFloatingAwayCoroutine());
        if (!IsInvoking()){
            InvokeRepeating("moveHitResult", 0f, 0.01f);
        }
    }

}
