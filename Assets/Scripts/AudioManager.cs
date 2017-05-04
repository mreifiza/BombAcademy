using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public float masterVolume { get; private set; }
    public float musicVolume { get; private set; }
    public float sfxVolume { get; private set; }

    public static AudioManager instance;

    AudioSource[] audioSources;

    public AudioClip music;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            // Don't destroy Audio Manager for every scene
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Dynamically create new audio sources
            audioSources = new AudioSource[2];
            for (int i = 0; i < audioSources.Length; i++)
            {
                GameObject newAudioSource = new GameObject("Audio Source " + (i + 1));
                audioSources[i] = newAudioSource.AddComponent<AudioSource>();
                newAudioSource.transform.parent = transform;
            }

            // Get volume settings from PlayerPrefs
            masterVolume = PlayerPrefs.GetFloat("master vol", 1f);
            musicVolume = PlayerPrefs.GetFloat("music vol", 1f);
            sfxVolume = PlayerPrefs.GetFloat("sfx vol", 1f);
        }

    }

    private void Start()
    {
        PlayMusic();
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        audioSources[0].volume = masterVolume * musicVolume;
        audioSources[1].volume = masterVolume * sfxVolume;
        PlayerPrefs.SetFloat("master vol", masterVolume);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = masterVolume * value;
        audioSources[0].volume = musicVolume;
        PlayerPrefs.SetFloat("music vol", musicVolume);
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = masterVolume * value;
        audioSources[1].volume = sfxVolume;
        PlayerPrefs.SetFloat("sfx vol", sfxVolume);
    }
	
    public void PlayMusic()
    {
        audioSources[0].clip = music;
        audioSources[0].Play();
        Invoke("PlayMusic", music.length);
    }
}
