using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public int dmgAmount;
    public GameObject impactEffect;
    void Start()
    {
        Vector3 dir = transform.position - PlayerHealthController.instance.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        AudioManager.instance.PlaySFXAdjusted(2);
    }

    void Update()
    {
        rb.velocity = -transform.right * moveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(dmgAmount);
        }
        if (impactEffect != null) 
        {
            Instantiate(impactEffect,transform.position, transform.rotation);
            Destroy(gameObject);
        }
        AudioManager.instance.PlaySFX(3);
    }
}
