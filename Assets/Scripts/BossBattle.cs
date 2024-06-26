using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    private CameraController cam;
    public Transform camPoint;
    public float camSpeed;

    public int threshold1, threshold2;
    public float activeTime, fadeOutTime, inactiveTime,moveSpeed;
    private float activeCounter, fadeCounter, inactiveCounter;
    public Transform[] spawnPoints;
    private Transform targetPoint;
    public Animator anim;
    public Transform boss;
    public float timeBetweenShots1, timeBetweenShots2;
    private float shotCounter;
    public GameObject bullet;
    public Transform shotPoint;
    public GameObject[] winObjs;
    private bool battleEnded;
    public string bossRef;
    void Start()
    {
        cam =  FindObjectOfType<CameraController>();
        cam.enabled= false;

        activeCounter = activeTime;
        shotCounter = timeBetweenShots1;

        AudioManager.instance.PlayBossMusic();
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, camPoint.position, camSpeed * Time.deltaTime);
        if (!battleEnded)
        {
            if (BossHealthController.instance.currentHp > threshold1)
            {
                if (activeCounter > 0)
                {
                    activeCounter -= Time.deltaTime;
                    if (activeCounter <= 0)
                    {
                        fadeCounter = fadeOutTime;
                        anim.SetTrigger("vanish");
                    }
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        shotCounter = timeBetweenShots1;
                        Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    }
                }
                else if (fadeCounter > 0)
                {
                    fadeCounter -= Time.deltaTime;
                    if (fadeCounter <= 0)
                    {
                        boss.gameObject.SetActive(false);
                        inactiveCounter = inactiveTime;
                    }
                }
                else if (inactiveCounter > 0)
                {
                    inactiveCounter -= Time.deltaTime;
                    if (inactiveCounter <= 0)
                    {
                        boss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                        boss.gameObject.SetActive(true);
                        activeCounter = activeTime;
                        shotCounter = timeBetweenShots1;
                    }
                }
            }
            else
            {
                if (targetPoint == null)
                {
                    targetPoint = boss;
                    fadeCounter = fadeOutTime;
                    anim.SetTrigger("vanish");
                }
                else
                {
                    if (Vector3.Distance(boss.position, targetPoint.position) > .02f)
                    {
                        boss.position = Vector3.MoveTowards(boss.position, targetPoint.position, moveSpeed * Time.deltaTime);
                        if (Vector3.Distance(boss.position, targetPoint.position) <= .02f)
                        {
                            fadeCounter = fadeOutTime;
                            anim.SetTrigger("vanish");
                        }
                        shotCounter -= Time.deltaTime;
                        if (shotCounter <= 0)
                        {
                            if (BossHealthController.instance.currentHp > threshold1)// playerhealthcontroller 
                            {
                                shotCounter = timeBetweenShots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShots2;
                            }
                            Instantiate(bullet, shotPoint.position, Quaternion.identity);
                        }
                    }
                    else if (fadeCounter > 0)
                    {
                        fadeCounter -= Time.deltaTime;
                        if (fadeCounter <= 0)
                        {
                            boss.gameObject.SetActive(false);
                            inactiveCounter = inactiveTime;
                        }
                    }
                    else if (inactiveCounter > 0)
                    {
                        inactiveCounter -= Time.deltaTime;
                        if (inactiveCounter <= 0)
                        {
                            boss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                            targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                            int whileBreaker = 0;
                            while (targetPoint.position == boss.position && whileBreaker < 100)
                            {
                                targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                                whileBreaker++;
                            }

                            boss.gameObject.SetActive(true);

                            if (BossHealthController.instance.currentHp > threshold1)// playerhealthcontroller 
                            {
                                shotCounter = timeBetweenShots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShots2;
                            }
                        }
                    }
                }
            }
        }
        else 
        {
            fadeCounter -= Time.deltaTime;
            if (fadeCounter < 0)
            {
                if (winObjs != null)
                {
                    foreach (GameObject obj in winObjs)
                    {
                        obj.SetActive(!obj.activeSelf);
                        obj.transform.SetParent(null);
                    }
                }
                cam.enabled = true;
                gameObject.SetActive(false);

                AudioManager.instance.PlayLevelMusic();

                PlayerPrefs.SetInt(bossRef, 1);
            }
        }
    }
    public void EndBattle() 
    {
        battleEnded= true;
        fadeCounter = fadeOutTime;
        anim.SetTrigger("vanish");
        boss.GetComponent<Collider2D>().enabled = false;

        BossBulletController[] bullets = FindObjectsOfType<BossBulletController>();
        if (bullets.Length > 0)
        {
            foreach (BossBulletController b in bullets)
            {
                Destroy(b.gameObject);
            }
        }
        //gameObject.SetActive(false);
    }
}
