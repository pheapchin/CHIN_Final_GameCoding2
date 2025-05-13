using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    AudioSource bkgMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MusicGotterdammerung()
    {
        print("Now Playing Gotterdammerung");
        gameObject.GetComponents<AudioSource>()[0].enabled = true;
    }
}
