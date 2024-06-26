using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupController : MonoBehaviour
{
    public int healAmount;
    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player") PlayerHealthController.instance.HealPlayer(healAmount);
        if(pickupEffect!=null) Instantiate(pickupEffect,transform.position, Quaternion.identity);
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(5);
    }
}
