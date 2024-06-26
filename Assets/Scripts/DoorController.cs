using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public Animator anim;
    public float distanceToOpen;
    private PlayerController player;
    private bool playerExited;
    public Transform exitPoint;
    public float movePlayerSpeed;
    public string lvlToLoad;
    void Start()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceToOpen) anim.SetBool("doorOpen", true);
        else anim.SetBool("doorOpen", false);
        if(playerExited)  player.transform.position = Vector3.MoveTowards(player.transform.position, exitPoint.transform.position, movePlayerSpeed * Time.deltaTime); 
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player") 
        {
            if (!playerExited) 
            {
                player.canMove = false;
                StartCoroutine(UseDoorCo());
            }
        }
    }

    IEnumerator UseDoorCo() 
    {
        playerExited= true;
        player.anim.enabled = false;
        UIController.instance.StartFadeToBlack();
        yield return new WaitForSeconds(1.5f);
    
        RespawnController.instance.SetSpawnPoint(exitPoint.position);
        player.canMove = true;
        player.anim.enabled = true;

        UIController.instance.StartFadeFromBlack();

        PlayerPrefs.SetString("ContinueLevel", lvlToLoad);
        PlayerPrefs.SetFloat("PosX",exitPoint.position.x);
        PlayerPrefs.SetFloat("PosY",exitPoint.position.y);
        PlayerPrefs.SetFloat("PosZ",exitPoint.position.z);


        SceneManager.LoadScene(lvlToLoad);
    }
}
