using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int dmgAmount = 1;
    public bool destroyOnDmg;
    public GameObject destroyEffect;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player") DealDamage();
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player") DealDamage();
    }
    void DealDamage() 
    {
        PlayerHealthController.instance.DamagePlayer(dmgAmount);
        if(destroyOnDmg) 
        {
            if (destroyEffect != null)
            {
                Instantiate(destroyEffect,transform.position,transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
