using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class GameUtils
{

    public static bool attackUnit(GameObject unit1_from_hit, GameObject unit2_to_hit,
             EventHandler<AttackResponse.OnPlayerAttackChangedArgs> OnEnemyAttack)
    {
        float damage = AttackResponse.calculateAttack(unit1_from_hit.GetComponent<Unit>().stat, unit2_to_hit.GetComponent<Unit>().stat);
        float getBarValue = damage * 0.3f;
     
        unit1_from_hit.GetComponent<Unit>().hp -= getBarValue;
        getBarValue /= 100;
        unit1_from_hit.transform.Find("UI/life").GetComponent<Scrollbar>().size -= getBarValue;
       
        unit2_to_hit.GetComponent<Animator>()?.SetTrigger("attack");
      
        return damage > 0;
    }

    public static IEnumerator damageFlashingColor(Color originalColor, SpriteRenderer image)
    {
        float timer = 0f;

        if (image != null)
        {
            // Sebzés színére váltás
            while (timer < 2.0f)
            {
                if (image == null)
                    yield break;
                    image.color = Color.Lerp(originalColor, Color.red, Mathf.PingPong(Time.time * 2, 1));
                    timer += Time.deltaTime;
                
                yield return null;
            }

            // Vissza az eredeti színre
            image.color = originalColor;
        }
        yield return null;
    }
}

