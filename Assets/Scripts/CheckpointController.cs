using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player") RespawnController.instance.SetSpawnPoint(transform.position);
    }
}
