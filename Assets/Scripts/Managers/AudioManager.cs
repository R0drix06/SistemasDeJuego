using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource sfxObject;

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
}
