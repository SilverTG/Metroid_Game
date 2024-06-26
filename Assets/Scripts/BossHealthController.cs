using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController instance;
    private void Awake()
    {
        instance = this;
    }
    public Slider bossHpSlider;
    public int currentHp = 30;

    public BossBattle boss;

    void Start()
    {
        bossHpSlider.maxValue = currentHp;
        bossHpSlider.value = currentHp;
    }

    void Update()
    {
        
    }
    public void TakeDmg(int dmgAmount) 
    {
        currentHp -= dmgAmount;
        if (currentHp <= 0)
        {
            boss.EndBattle();
            AudioManager.instance.PlaySFX(0);
        }
        else AudioManager.instance.PlaySFX(1);
        bossHpSlider.value = currentHp;
    }
}
