using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float timeToExplode = .5f,blastRange;
    public GameObject explosion;
    public LayerMask whatIsDestructible, whatIsDamageable;
    public int dmgAmount;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToExplode-= Time.deltaTime;
        if (timeToExplode <= 0) 
        {
            if (explosion != null) Instantiate(explosion,transform.position,transform.rotation);
            
            Destroy(gameObject);

            Collider2D[] objToDamage = Physics2D.OverlapCircleAll(transform.position, blastRange, whatIsDestructible);

            if (objToDamage.Length > 0) 
            {
                foreach(Collider2D obj in objToDamage) 
                {
                    Destroy(obj.gameObject);
                }
            }

            Collider2D[] objectsToDmg = Physics2D.OverlapCircleAll(transform.position, blastRange,whatIsDamageable);
            foreach(Collider2D obj in objectsToDmg) 
            {
                EnemyHealthController enemyHp = obj.GetComponent<EnemyHealthController>();
                if (enemyHp != null) enemyHp.DamageEnemy(dmgAmount);
            }
            AudioManager.instance.PlaySFXAdjusted(4);
        }
    }
}
