using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    public Slider hpSlider;
    public Image fadeScreen;
    public float fadeSpeed = 2f;
    private bool fadingToBlack,fadingFromBlack;
    public string mainMenuScene;
    public GameObject pauseScreen;

    void Start()
    {
        //UpdateHp(PlayerHealthController.instance.currentHp, PlayerHealthController.instance.maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        if(fadingToBlack) 
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a,1f,fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f) fadingToBlack= false;
        }
        else if(fadingFromBlack) 
        { 
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f) fadingFromBlack = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }

    }

    public void UpdateHp(int currentHp,int maxHp) 
    {
        hpSlider.maxValue = maxHp;
        hpSlider.value = currentHp;
    }

    public void StartFadeToBlack() 
    {
        fadingToBlack= true;
        fadingFromBlack= false;
    }
    public void StartFadeFromBlack() 
    {
        fadingToBlack= false;
        fadingFromBlack= true;
    }
    public void PauseUnpause() 
    {
        if (!pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else 
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void GoToMainMenu() 
    {
        Time.timeScale = 1f;
        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;
        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;

        instance = null;
        Destroy(gameObject);

        SceneManager.LoadScene(mainMenuScene);
    }
}
