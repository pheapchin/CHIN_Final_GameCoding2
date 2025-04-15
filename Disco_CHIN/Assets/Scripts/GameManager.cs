using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        //checks if there is already a game managaer
        if (Instance == null)
        {
            //we are going to assign
            Instance = this;
            //keeps game manager alive when switching scenes
            DontDestroyOnLoad(this.gameObject);

            //SceneManager.sceneLoaded += OnSceneLoad;
        }
        else
        {
            //destroy duplicate game managers
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
