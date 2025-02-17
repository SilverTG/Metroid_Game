using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D rb;
    public Vector2 moveDir;
    public GameObject impactEffect;
    public int dmgAmount = 1;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveDir * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Enemy")
        {
            coll.GetComponent<EnemyHealthController>().DamageEnemy(dmgAmount);
        }
        if (coll.tag == "Boss") 
        {
            BossHealthController.instance.TakeDmg(dmgAmount);
        }
        if(impactEffect!= null) Instantiate(impactEffect, transform.position, Quaternion.identity);
        AudioManager.instance.PlaySFXAdjusted(3);
        Destroy(gameObject);
    }
    private void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}
