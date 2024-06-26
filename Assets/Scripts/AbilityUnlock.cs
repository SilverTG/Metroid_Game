using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityUnlock : MonoBehaviour
{
    public bool unlockDoubleJump,unlockDash,unlockBecomeBall,unlockDropBomb;
    public GameObject pickupEffect;
    public string unlockMsg;
    public TMP_Text unlockText;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerAbilityTracker player = col.GetComponentInParent<PlayerAbilityTracker>();
            if (unlockDoubleJump) { player.canDoubleJump = true; PlayerPrefs.SetInt("DoubleJumpUnlocked", 1); }
            if (unlockDash) { player.canDash= true; PlayerPrefs.SetInt("DashUnlocked", 1); }
            if (unlockBecomeBall) { player.canBecomeBall = true; PlayerPrefs.SetInt("BecomeBallUnlocked", 1); }
            if (unlockDropBomb) {player.canDropBomb= true; PlayerPrefs.SetInt("DropBombUnlocked", 1); }
            Instantiate(pickupEffect, transform.position, transform.rotation);
            unlockText.transform.parent.SetParent(null);
            unlockText.transform.position = new Vector3(transform.position.x,transform.position.y + 2f,transform.position.z);
            unlockText.text = unlockMsg;
            unlockText.gameObject.SetActive(true);
            Destroy(unlockText.transform.parent.gameObject, 3f);
            Destroy(gameObject);    
            AudioManager.instance.PlaySFX(5);
        }
    }
}
