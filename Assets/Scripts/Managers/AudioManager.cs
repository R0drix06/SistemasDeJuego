using System;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource sfxObject;

    public event Action OnSongFinished;

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


    public void PlaySFXSound(AudioClip clip, Transform spawnTransform, float volume)
    {
        AudioSource audiosource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);

        audiosource.clip = clip;
        audiosource.volume = volume;
        audiosource.Play();
        
        float clipLength = audiosource.clip.length;

        Destroy(audiosource.gameObject, clipLength);
    }

    public void PlayMusic(AudioSource source, AudioClip clip, Transform spawnTransform, float volume)
    {
        source.clip = clip;
        source.volume = volume;
        source.loop = true;
        source.Play();

        float clipLength = source.clip.length;

    }
}
