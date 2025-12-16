using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;

    [SerializeField] public List<AudioClip> soundTrack;

    private AudioSource source;

    int currentIndex = 0;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //CustomUpdateManager.Instance.Register(this);
        source = GetComponent<AudioSource>();
        AudioManager.Instance.PlayMusic(source, soundTrack[currentIndex], transform, 0.1f);
        //AudioManager.Instance.OnSongFinished += () => IndexFunction();
    }


    public void IndexFunction()
    {
        if (currentIndex == soundTrack.Count)
            currentIndex = 0;
        else
            currentIndex++;

        AudioManager.Instance.PlayMusic(source, soundTrack[currentIndex], transform, 0.1f);
    }

    //private void OnDestroy()
    //{
    //    CustomUpdateManager.Instance.Unregister(this);
    //}
}
