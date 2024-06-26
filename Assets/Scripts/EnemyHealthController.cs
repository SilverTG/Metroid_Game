using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int totalHp = 3;
    public GameObject deathEffect;

    public void DamageEnemy(int dmg) 
    {
        totalHp -= dmg;
        if (totalHp <= 0)
        {
            if(deathEffect != null) Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(4);
        }
    }
}
