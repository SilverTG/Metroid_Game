using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else Destroy(gameObject);
    }
    public GameObject[] maps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateMap(string mapToActivate) 
    {
        foreach (GameObject map in maps) 
        {
            if (map.name == mapToActivate) { map.SetActive(true);  }
        }
    }
}
