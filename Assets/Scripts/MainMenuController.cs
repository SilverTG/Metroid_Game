using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string newGameScene;
    public GameObject continueBtn;
    public PlayerAbilityTracker player;
    void Start()
    {
        if(PlayerPrefs.HasKey("ContinueLevel")) continueBtn.SetActive(true);
        AudioManager.instance.PlayMainMenuMusic();
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(newGameScene);
    }
    public void ContinueGame()
    {
        player.gameObject.SetActive(true);
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
        if (PlayerPrefs.HasKey("DoubleJumpUnlocked")) { if (PlayerPrefs.GetInt("DoubleJumpUnlocked") == 1) player.canDoubleJump = true; }
        if (PlayerPrefs.HasKey("DashUnlocked")) { if (PlayerPrefs.GetInt("DashUnlocked") == 1) player.canDash = true; }
        if (PlayerPrefs.HasKey("BecomeBallUnlocked")) { if (PlayerPrefs.GetInt("BecomeBallUnlocked") == 1) player.canBecomeBall = true; }
        if (PlayerPrefs.HasKey("DropBombUnlocked")) { if (PlayerPrefs.GetInt("DropBombUnlocked") == 1) player.canDropBomb = true; }

        SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));
    }
    public void QuitGame() 
    {
        Application.Quit();
    }
 
}
