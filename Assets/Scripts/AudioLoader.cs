using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    public AudioManager am;
    private void Awake()
    {
        if (AudioManager.instance == null) 
        {
            AudioManager newAm = Instantiate(am);
            AudioManager.instance = newAm;
            DontDestroyOnLoad(newAm.gameObject);
        }
    }

}
