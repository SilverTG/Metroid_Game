using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public int currentHp,maxHp;
    public float invincibilityDuratioin,flashDuration;
    private float invincibilityCounter,flashCounter;

    public SpriteRenderer[] playerSprites;

    void Start()
    {
        currentHp = maxHp;
    }

    void Update()
    {
        if (invincibilityCounter > 0) 
        {
            invincibilityCounter -= Time.deltaTime; 
            flashCounter-= Time.deltaTime;
            if (flashCounter <= 0) 
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;
                }
                flashCounter = flashDuration;
            }
            if (invincibilityCounter <= 0) 
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;
                }
                flashCounter= 0f;
            }
        }
    }

    public void DamagePlayer(int dmg) 
    {
        if (invincibilityCounter <= 0)
        {
            currentHp -= dmg;
            if (currentHp <= 0)
            {
                currentHp = 0;
                RespawnController.instance.Respawn();
                AudioManager.instance.PlaySFX(8);
            }
            else
            {
                invincibilityCounter = invincibilityDuratioin;
                AudioManager.instance.PlaySFX(11);
            }
            UIController.instance.UpdateHp(currentHp, maxHp);
        }
    }

    public void FillHp() 
    {
        currentHp = maxHp;
        UIController.instance.UpdateHp(currentHp, maxHp);
    }

    public void HealPlayer(int healAmount) 
    {
        currentHp += healAmount;
        if(currentHp>maxHp) currentHp = maxHp;
        UIController.instance.UpdateHp(currentHp, maxHp);
    }
}
